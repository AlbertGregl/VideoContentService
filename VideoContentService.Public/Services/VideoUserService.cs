using IntegrationModule.BLModels;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using VideoContentService.Public.Properties;

namespace VideoContentService.Public.Services
{
    public class VideoUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public VideoUserService(HttpClient httpClient, IOptions<ApiConfig> apiConfig)
        {
            _httpClient = httpClient;
            _baseUrl = apiConfig.Value.BaseUrl;
        }

        public async Task<IEnumerable<VideoResponse>> GetAllVideosAsync(string name, string genre, string orderBy, int page, int pageSize, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync($"{_baseUrl}/Videos/GetAll?name={name}&genre={genre}&orderBy={orderBy}&page={page}&pageSize={pageSize}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<VideoResponse>>(content);
        }

    }
}
