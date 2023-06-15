using IntegrationModule.BLModels;
using IntegrationModule.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text;
using VideoContentService.Admin.Properties;

namespace VideoContentService.Admin.Services
{
    public class UserService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public UserService(HttpClient httpClient, IOptions<ApiConfig> apiConfig)
        {
            _httpClient = httpClient;
            _baseUrl = apiConfig.Value.BaseUrl;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync(string firstNameFilter = null, string lastNameFilter = null, string usernameFilter = null, string countryFilter = null)
        {
            var queryString = new List<string>();

            if (!string.IsNullOrEmpty(firstNameFilter))
                queryString.Add($"firstNameFilter={firstNameFilter}");

            if (!string.IsNullOrEmpty(lastNameFilter))
                queryString.Add($"lastNameFilter={lastNameFilter}");

            if (!string.IsNullOrEmpty(usernameFilter))
                queryString.Add($"usernameFilter={usernameFilter}");

            if (!string.IsNullOrEmpty(countryFilter))
                queryString.Add($"countryFilter={countryFilter}");

            var query = string.Join("&", queryString);

            var response = await _httpClient.GetAsync($"{_baseUrl}/ManageUsers/GetAll?{query}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IEnumerable<UserResponse>>(content);
        }

        public async Task<UserResponse> GetUserByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/ManageUsers/GetById/{id}");
            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<UserResponse>(content);
        }

        public async Task CreateUserAsync(UserRequest user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            await _httpClient.PostAsync($"{_baseUrl}/ManageUsers/Create", content);
        }

        public async Task UpdateUserAsync(int id, UserRequest user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            await _httpClient.PutAsync($"{_baseUrl}/ManageUsers/Update/{id}", content);
        }

        public async Task SoftDeleteUserAsync(int id)
        {
            await _httpClient.DeleteAsync($"{_baseUrl}/ManageUsers/SoftDelete/{id}");
        }
    }
}