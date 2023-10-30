using BankWEB.DataContext;

namespace BankWEB.Data
{
    public static class DbInitializer
    {
        public static void Initialize(AuthBankContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}
