using System.ComponentModel.DataAnnotations;

namespace BankWEB.Models
{
    public class UserLogin
    {
        [Required, MaxLength(50)]
        public string UsernameProp { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
