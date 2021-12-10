using Shopping.Aggregator.Extensions;
using Shopping.Aggregator.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Shopping.Aggregator.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        public BasketService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<BasketModel> GetBasket(string username)
        {
            var response = await _httpClient.GetAsync($"api/Baket/{username}");
            return await response.ReadContentAs<BasketModel>();
        }
    }
}
