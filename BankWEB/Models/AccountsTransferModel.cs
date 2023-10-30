using BankWEB.Models.Enums;

namespace BankWEB.Models
{
    public class AccountsTransferModel
    {
        public int FromAccountId { get; set; } = -1;
        public int ToAccountId { get; set; } = -1;
        public decimal Money { get; set; }
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
    }
}
