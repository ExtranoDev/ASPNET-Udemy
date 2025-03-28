using StockAppProject.ServiceContracts;
using System.Text.Json;

namespace StockAppProject.Services
{
    public class FinnhubService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : IFinnhubService
    {
        public Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol)
        {            
            return GetProprerties(stockSymbol, "https://finnhub.io/api/v1/stock/profile2?symbol=");
        }

        public Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol)
        {   
            return GetProprerties(stockSymbol, "https://finnhub.io/api/v1/quote?symbol=");
        }

        private async Task<Dictionary<string, object>?> GetProprerties(string stockSymbol, string uri)
        {            
            using (HttpClient httpClient = httpClientFactory.CreateClient())
            {
                HttpRequestMessage httpRequestMessage = new()
                {
                    RequestUri = new Uri($"{uri}{stockSymbol}&token={configuration["FinnhubToken"]}"),
                    Method = HttpMethod.Get
                };

                HttpResponseMessage httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                Stream stream = httpResponseMessage.Content.ReadAsStream();

                StreamReader streamReader = new(stream);
                string response = streamReader.ReadToEnd();
                Dictionary<string, object>? responseDictionary =
                JsonSerializer.Deserialize<Dictionary<string, object>>(response);

                // error handling
                if (responseDictionary == null) throw new InvalidOperationException("No response from finnhub server");
                if (responseDictionary.TryGetValue("error", out object? value)) throw new InvalidOperationException(Convert.ToString(value));

                return responseDictionary;
            }            
        }
    }
}
