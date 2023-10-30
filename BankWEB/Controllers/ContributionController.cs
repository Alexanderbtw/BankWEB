using BankWEB.Interfaces;
using BankWEB.Models;
using BankWEB.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Caching.Memory;
using System.Net;

namespace BankWEB.Controllers
{
    [Authorize]
    public class ContributionController : Controller
    {
        private readonly IMemoryCache cache;
        private readonly IBankData bankData;

        public ContributionController(IMemoryCache _cache, IBankData _bd)
        {
            cache = _cache;
            bankData = _bd;
        }

        [HttpGet]
        public IActionResult Contribution()
        {
            cache.TryGetValue(User.Identity.Name, out Client? client);
            if (client == null)
                return NotFound();
            ViewData["Accounts"] = new SelectList(client.Accounts, nameof(Account.Id), nameof(Account.Title));
            ViewData["Currency"] = client.Accounts.Select(account => account.Currency.ToString()).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contribution(ContributionData data)
        {
            if (!cache.TryGetValue(User.Identity.Name, out Client? client))
                return BadRequest("User Not Find!");
            data.OwnerId = client!.Id;
            data.ParentAccount = client.Accounts.First(a => a.Id == data.AccountId);
            data.Currency = data.ParentAccount.Currency;
            data.Duration = Converters.DurationToPercents[data.Percents];
            data.EndDate = DateOnly.FromDateTime(DateTime.Today + data.Duration);
           

            if (data.ParentAccount.Money < data.Money)
            {
                ModelState.AddModelError("AccountId", "Infufficient funds");
                ViewData["Accounts"] = new SelectList(client.Accounts, nameof(Account.Id), nameof(Account.Title));
                ViewData["Currency"] = client.Accounts.Select(account => account.Currency.ToString()).ToList();
                return View(data);
            }

            var createTask = bankData.CreateContributionAsync(data);

            if (await createTask == HttpStatusCode.OK)
            {
                cache.Remove(User.Identity.Name);
                return View("OperationResult", data);
            }

            return BadRequest(createTask.Exception?.InnerException);
        }
    }
}
