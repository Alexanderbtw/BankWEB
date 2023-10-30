using BankWEB.Interfaces;
using BankWEB.Models;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace BankWEB.Data
{
    public class BankDataAPI : IBankData
    {
        private HttpClient httpClient { get; set; }
        //private string url = @"https://localhost:44314/api/values";

        public BankDataAPI(IHttpClientFactory factory)
        {
            httpClient = factory.CreateClient("BankAPI");
        }

        public async Task<HttpStatusCode> AddClientAsync(Client client)
        {
            using var r = await httpClient.PostAsJsonAsync(
                requestUri: "",
                value: client);
            return r.StatusCode;
        }

        public async Task<Client> GetClientByUsernameAsync(string username)
        {
            //Через Header
            //httpClient.DefaultRequestHeaders.Remove("Username");
            httpClient.DefaultRequestHeaders.Add("Username", username);
            var client = await httpClient.GetFromJsonAsync<Client>("");
            client!.Accounts = client.Accounts.OrderBy(a => a.Id).ToList();
            return client!;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            var clients = await httpClient.GetFromJsonAsync<Client[]>("all");
            return clients!;
        }

        public async Task<HttpStatusCode> CreateAccountAsync(Account account)
        {
            using var res = await httpClient.PostAsync(
                requestUri: $"accounts",
                content: new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, mediaType: "application/json")
                );
            return res.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteAccountAsync(int accountId)
        {
            using var res = await httpClient.DeleteAsync(requestUri: $"accounts?accountId={accountId}");
            return res.StatusCode;
        }

        public async Task<HttpStatusCode> ReplenishmentAsync(int accountId, decimal money)
        {
            var account = new Account() { Id = accountId, Money = money };

            using var r = await httpClient.PutAsync(
                requestUri: "accounts",
                content: new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, mediaType: "application/json")
                );

            return r.StatusCode;
        }

        public async Task<HttpStatusCode> WithdrawAsync(int accountId, decimal money)
        {
            return await ReplenishmentAsync(accountId, money * -1);
        }

        public async Task<HttpStatusCode> AccountTransferAsync(AccountsTransferModel model)
        {
            using var r = await httpClient.PutAsJsonAsync(
                requestUri: $"transfers",
                value: model
                );

            return r.StatusCode;
        }

        public async Task<HttpStatusCode> GlobalTransferAsync(GlobalTransferModel data)
        {
            using var r = await httpClient.PutAsJsonAsync(
                requestUri: $"transaction",
                value: data);

            return r.StatusCode;
        }

        public async Task<HttpStatusCode> SetAvatarAsync(IFormFile file, Client client)
        {
            if (file != null && file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    await file.CopyToAsync(ms);
                    byte[] fileBytes = ms.ToArray();
                    if (client.Avatar != null)
                        client.Avatar.Image = fileBytes;
                    else
                        client.Avatar = new Avatar() { Image = fileBytes };

                    client.Accounts = new List<Account>();
                    client.Contributions = new List<ContributionData>();

                    using var r = await httpClient.PutAsJsonAsync("", client);
                    return r.StatusCode;
                }
            }
            return HttpStatusCode.BadRequest;
        }

        public async Task<HttpStatusCode> CreateContributionAsync(ContributionData data)
        {
            using var r = await httpClient.PostAsJsonAsync(
                requestUri: $"contribution",
                value: data
                );

            return r.StatusCode;
        }
    }
}
