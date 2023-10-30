using BankWEB.Models.Enums;
using MessagePack.Formatters;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace BankWEB.Models
{
    public class Account
    {
        public int Id { get; set; } = 0;

        public string Title { get; set; } = string.Empty;

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:N2}")]
        public decimal Money { get; set; } = 0;

        public Currency Currency { get; set; } = Currency.RUB;

        public int OwnerId { get; set; } = 0;
    }
}
