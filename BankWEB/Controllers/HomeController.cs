using Azure;
using Azure.Core;
using BankWEB.Attributes;
using BankWEB.Interfaces;
using BankWEB.Models;
using BankWEB.Models.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Net;

namespace BankWEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IBankData bankData;
        private readonly IMemoryCache cache;

        public HomeController(IBankData _bd, IMemoryCache memoryCache)
        {
            cache = memoryCache;
            bankData = _bd;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            cache.TryGetValue(User.Identity.Name, out Client? client);
            if (client == null)
            {
                client = await bankData.GetClientByUsernameAsync(User.Identity.Name);
                cache.Set(User.Identity.Name, client, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)));
            }

            if (client.Avatar != null)
            {
                cache.TryGetValue($"{User.Identity.Name}.AvatarSrc", out string? imgSrc);
                if (imgSrc == null)
                {
                    var base64 = Convert.ToBase64String(client.Avatar.Image);
                    imgSrc = String.Format("data:image/gif;base64,{0}", base64);
                    cache.Set($"{User.Identity.Name}.AvatarSrc", imgSrc, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(30)));
                }
                
                ViewData["Avatar"] = imgSrc;
            }

            return View(client);
        }

        [HttpGet]
        public async Task<IActionResult> Transfers()
        {
            cache.TryGetValue(User.Identity.Name, out Client? client);
            if (client == null)
            {
                client = await bankData.GetClientByUsernameAsync(User.Identity.Name);
                if (client != null)
                {
                    cache.Set(User.Identity.Name, client, new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)));
                }
            }
            ViewData.Add("Clients", (await bankData.GetAllClientsAsync()).Where(c => c.Id != client.Id).OrderBy(c => c.Username));
            return View(client);
        }

        [HttpPost]
        public async Task<IActionResult> Transfers([FromBody] GlobalTransferModel data)
        {
            cache.TryGetValue(User.Identity.Name, out Client? client);
            data.Commission = Converters.StatusToCommission[client!.Status];

            var res = await bankData.GlobalTransferAsync(data);
            cache.Remove(User.Identity.Name);

            //Чтобы остаться на той же странице
            //var redirectUrl = this.Url.Action();
            //return Json(new { Url = redirectUrl });

            return ShowResult(res, data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount(Account account)
        {
            // Проверка аккаунта на уникальность
            cache.TryGetValue(User.Identity.Name, out Client? client);
            if (client == null || client.Accounts.FirstOrDefault(acc => acc.Title == account.Title) != null)
            {
                throw new ArgumentException("There is already an account with this name");
            }
            var res = await bankData.CreateAccountAsync(account);
            return ShowResult(res, null, "Account creation");
        }

        [HttpGet]
        [Route("[controller]/[action]/{accountId:int}")]
        public async Task<IActionResult> DeleteAccount([FromRoute]int accountId)
        {
            cache.TryGetValue(User.Identity.Name, out Client? client);
            if (client == null) return NotFound();

            var account = client.Accounts.First(c => c.Id == accountId);
            var res = await bankData.DeleteAccountAsync(accountId);
            return ShowResult(res, account, "Account deletion");
        }

        [HttpPost]
        public async Task<IActionResult> ReplenishmentAccount(int accountId, decimal money)
        {
            var res = await bankData.ReplenishmentAsync(accountId, money);
            return ShowResult(res, null, "Replenishment");
        }

        [HttpPost]
        public async Task<IActionResult> WithdrawAccount(int accountId, decimal money)
        {
            var res = await bankData.WithdrawAsync(accountId, money);
            return ShowResult(res, null, "Withdraw");
        }

        [HttpPost]
        public async Task<IActionResult> TransferBetwAccount(int fromAccountId, decimal money, int toAccountId)
        {
            cache.TryGetValue(User.Identity.Name, out Client? client);
            if (client == null) return NotFound();
            AccountsTransferModel model = new AccountsTransferModel() { 
                FromAccountId = fromAccountId, 
                ToAccountId = toAccountId,
                Money = money,
                FromCurrency = client.Accounts.First(a => a.Id == fromAccountId).Currency,
                ToCurrency = client.Accounts.First(a => a.Id == toAccountId).Currency
            };
            var res = await bankData.AccountTransferAsync(model);
            return ShowResult(res, model);
        }

        [HttpPost]
        public async Task<IActionResult> SetAvatar(IFormFile image)
        {
            cache.TryGetValue(User.Identity.Name, out Client? client);
            var res = HttpStatusCode.BadRequest;
            if (client != null)
            {
                res = await bankData.SetAvatarAsync(image, client);
                cache.Remove($"{User.Identity.Name}.AvatarSrc");
            }

            return ShowResult(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [NonAction]
        public IActionResult ShowResult(HttpStatusCode res, object? model = null, string OperationTitle = "Operation")
        {
            if (res != HttpStatusCode.OK)
            {
                return StatusCode((int)res);
            }
            cache.Remove(User.Identity.Name);

            ViewData["Operation"] = OperationTitle;

            return PartialView("OperationResult", model);
        }
    }
}