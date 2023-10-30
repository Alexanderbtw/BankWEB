using BankWEB.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BankWEB.Infrastructure
{
    public class GlobalTransferModelBinderProvider : IModelBinderProvider
    {
        private readonly IModelBinder _modelBinder = new GlobalTransferModelBinder();

        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(GlobalTransferModel) ? _modelBinder : null;
        }
    }
}
