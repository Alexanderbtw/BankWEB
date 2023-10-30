using BankWEB.Models.Enums;

namespace BankWEB.Models
{
    public class GlobalTransferModel
    {
        public int AccountId { get; set; }
        public Currency AccountCurrency { get; set; }
        public decimal Money { get; set; }
        public float Commission { get; set; }
        public int RecipientId{ get; set; }
    }
}
