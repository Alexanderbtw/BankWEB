using BankWEB.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BankWEB.Infrastructure
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder _modelBinder = new DecimalModelBinder();

        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(decimal) ? _modelBinder : null;
        }
    }
}
