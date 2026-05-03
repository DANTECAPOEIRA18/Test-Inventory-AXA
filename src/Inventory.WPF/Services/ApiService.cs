using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Inventory.WPF.Models;

namespace Inventory.WPF.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public ApiService()
        {
            _baseUrl = ConfigurationManager.AppSettings["ApiBaseUrl"];
            _http = new HttpClient();
        }

        public async Task<List<UserVm>> GetUsers()
        {
            var response = await _http.GetAsync($"{_baseUrl}users");

            if (!response.IsSuccessStatusCode)
                return new List<UserVm>();

            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<UserVm>>(json);
        }

        public async Task CreateUser(UserVm user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            await _http.PostAsync($"{_baseUrl}users", content);
        }

        public async Task<List<AreaVm>> GetAreas()
        {
            var response = await _http.GetAsync($"{_baseUrl}areas");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<AreaVm>>(json);
        }

        public async Task<List<RoleVm>> GetRoles()
        {
            var response = await _http.GetAsync($"{_baseUrl}roles");
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<RoleVm>>(json);
        }

        public async Task UpdateUserContact(Guid userId, string contact, Guid AreaId, Guid RoleId)
        {
            var body = new UserVm
            {
                Contact = contact,
                AreaId = AreaId,
                RoleId = RoleId
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PutAsync($"{_baseUrl}users/{userId}/contact", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                throw new Exception($"Error updating contact: {error}");
            }
        }
    }
}