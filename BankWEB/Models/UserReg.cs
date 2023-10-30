using BankWEB.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BankWEB.Models
{
    public class UserReg
    {
        [Required, MaxLength(50)]
        public string UsernameProp { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public Status Status { get; set; }
    }
}
