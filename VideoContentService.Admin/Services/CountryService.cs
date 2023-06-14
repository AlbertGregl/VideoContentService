using IntegrationModule.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using VideoContentService.Admin.Properties;

namespace VideoContentService.Admin.Services
{
    public class CountryService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CountryService(HttpClient httpClient, IOptions<ApiConfig> apiConfig)
        {
            _httpClient = httpClient;
            _baseUrl = apiConfig.Value.BaseUrl;
        }

        // GET: api/Countries
        public async Task<IEnumerable<Country>> GetAllCountriesAsync(int page)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Countries?page={page}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<Country>>(content);
        }
    }
}
