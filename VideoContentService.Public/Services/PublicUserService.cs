using IntegrationModule.BLModels;
using IntegrationModule.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using VideoContentService.Public.Properties;

namespace VideoContentService.Public.Services
{
    public class PublicUserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public PublicUserService(HttpClient httpClient, IOptions<ApiConfig> apiConfig)
        {
            _httpClient = httpClient;
            _baseUrl = apiConfig.Value.BaseUrl;
        }

        public async Task<HttpResponseMessage> RegisterUserAsync(UserRegisterRequest user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync($"{_baseUrl}/Users/Register", content);
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequest user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/Users/Login", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserLoginResponse>(jsonResponse);
            }
            else
            {
                throw new Exception("Failed to login.");
            }
        }

        public async Task<Tokens> GetJwtTokensAsync(JwtTokensRequest request)
        {
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"{_baseUrl}/Users/JwtTokens", content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Tokens>(jsonResponse);
            }
            else
            {
                throw new Exception("Failed to retrieve tokens.");
            }
        }
    }
}


