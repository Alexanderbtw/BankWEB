using BankWEB.Models;
using BankWEB.Models.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace BankWEB.Infrastructure
{
    public class GlobalTransferModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            GlobalTransferModel data = new();
            data.AccountId = int.Parse(bindingContext.ValueProvider.GetValue("accountId").FirstValue!);
            data.AccountCurrency = (Currency)int.Parse(bindingContext.ValueProvider.GetValue("accountCurrency").FirstValue!);
            data.Money = decimal.Parse(bindingContext.ValueProvider.GetValue("money").FirstValue!, CultureInfo.InvariantCulture);
            data.RecipientId = int.Parse(bindingContext.ValueProvider.GetValue("recipientId").FirstValue!);
            bindingContext.Result = ModelBindingResult.Success(data);
            return Task.CompletedTask;
        }
    }
}
