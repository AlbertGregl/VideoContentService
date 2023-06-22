using IntegrationModule.BLModels;
using IntegrationModule.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using VideoContentService.Admin.Properties;

namespace VideoContentService.Admin.Services
{
    public class GenreService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public GenreService(HttpClient httpClient, IOptions<ApiConfig> apiConfig)
        {
            _httpClient = httpClient;
            _baseUrl = apiConfig.Value.BaseUrl;
        }

        public async Task<IEnumerable<GenreResponse>> GetAllGenresAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Genres/GetAll");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<GenreResponse>>(content);
        }

        public async Task CreateGenreAsync(GenreRequest genre)
        {
            var content = new StringContent(JsonConvert.SerializeObject(genre), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/Genres/Create", content);
        }

        public async Task UpdateGenreAsync(int id, GenreRequest genre)
        {
            var content = new StringContent(JsonConvert.SerializeObject(genre), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/Genres/Update/{id}", content);
        }

        public async Task DeleteGenreAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/Genres/Delete/{id}");
        }
    }
}
