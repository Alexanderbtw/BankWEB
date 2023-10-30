using BankWEB.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankWEB.Models
{
    public class ContributionData
    {
        public int Id { get; set; }

        public DateOnly EndDate { get; set; }

        [Required]
        public TimeSpan Duration { get; set; }

        [Required]
        public float Percents { get; set; }

        public Account? ParentAccount { get; set; }

        public int? AccountId { get; set; }

        [Range(10, 3_000_000)]
        public decimal Money { get; set; }

        public Currency? Currency { get; set; }

        public int OwnerId { get; set; }
    }
}
