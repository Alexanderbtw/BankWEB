using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace BankWEB.Infrastructure
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            string money = bindingContext.ValueProvider.GetValue("money").FirstValue;
            if (money != null)
            {
                bindingContext.Result = ModelBindingResult.Success(decimal.Parse(money, CultureInfo.InvariantCulture));
            }
            else
            {
                bindingContext.Result = ModelBindingResult.Success(0);
            }
            return Task.CompletedTask;
        }
    }
}
