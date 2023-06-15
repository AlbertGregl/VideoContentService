using IntegrationModule.BLModels;
using IntegrationModule.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using VideoContentService.Admin.Properties;

namespace VideoContentService.Admin.Services
{
    public class TagService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        // Inject the HttpClient and the API configuration.
        public TagService(HttpClient httpClient, IOptions<ApiConfig> apiConfig)
        {
            _httpClient = httpClient;
            _baseUrl = apiConfig.Value.BaseUrl;
        }

        public async Task<IEnumerable<TagResponse>> GetAllTagsAsync()
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Tags/GetAll");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<TagResponse>>(content);
        }

        public async Task<TagResponse> GetTagByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/Tags/GetById/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TagResponse>(content);
        }

        public async Task CreateTagAsync(TagRequest tag)
        {
            var content = new StringContent(JsonConvert.SerializeObject(tag), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/Tags/CreateTag", content);
        }

        public async Task UpdateTagAsync(int id, TagRequest tag)
        {
            var content = new StringContent(JsonConvert.SerializeObject(tag), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/Tags/UpdateTag/{id}", content);
        }

        public async Task DeleteTagAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/Tags/DeleteTag/{id}");
        }
    }
}
