using BankWEB.Models;
using System.Net;

namespace BankWEB.Interfaces
{
    public interface IBankData
    {
        Task<Client> GetClientByUsernameAsync(string username);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<HttpStatusCode> AddClientAsync(Client client);
        Task<HttpStatusCode> CreateAccountAsync(Account account);
        Task<HttpStatusCode> ReplenishmentAsync(int accountId, decimal money);
        Task<HttpStatusCode> WithdrawAsync(int accountId, decimal money);
        Task<HttpStatusCode> AccountTransferAsync(AccountsTransferModel model);
        Task<HttpStatusCode> GlobalTransferAsync(GlobalTransferModel data);
        Task<HttpStatusCode> SetAvatarAsync(IFormFile file, Client client);
        Task<HttpStatusCode> CreateContributionAsync(ContributionData data);
        Task<HttpStatusCode> DeleteAccountAsync(int accountId);
    }
}
