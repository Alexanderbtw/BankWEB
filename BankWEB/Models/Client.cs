using BankWEB.Models.Enums;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace BankWEB.Models
{
    public class Client
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public Status Status { get; set; }

        public Avatar? Avatar { get; set; }

        public ICollection<Account> Accounts { get; set; } = new Collection<Account>();

        public ICollection<ContributionData> Contributions { get; set; } = new Collection<ContributionData>();
    }
}
