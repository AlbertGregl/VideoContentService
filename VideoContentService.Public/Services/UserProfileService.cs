using IntegrationModule.BLModels;
using IntegrationModule.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using VideoContentService.Public.Properties;

namespace VideoContentService.Public.Services
{
    public class UserProfileService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserProfileService(HttpClient httpClient, IOptions<ApiConfig> apiConfig)
        {
            _httpClient = httpClient;
            _baseUrl = apiConfig.Value.BaseUrl;
        }

        // get user by id
        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ManageUsers/GetById/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserResponse>(content);
        }

        // change user password
        public async Task<HttpResponseMessage> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var jsonString = JsonConvert.SerializeObject(request);
            System.Diagnostics.Debug.WriteLine("Serialized JSON: " + jsonString); // Log the JSON string

            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync($"{_baseUrl}/Users/ChangePassword", content);
        }

    }
}
