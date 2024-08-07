using StaffMgtBlazorApp.Models;

namespace StaffMgtBlazorApp.Services
{
    public class StaffService:IStaffService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = $"https://localhost:7078/api/staff";

        public StaffService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<StaffDto>> GetStaffsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<StaffDto>>($"{_apiUrl}/");
        }

        public async Task<StaffDto> GetStaffAsync(string StaffId)
        {
            return await _httpClient.GetFromJsonAsync<StaffDto>($"{_apiUrl}/{StaffId}");
        }

        public async Task AddStaffAsync(StaffDto staff)
        {
            await _httpClient.PostAsJsonAsync($"{_apiUrl}/", staff);
        }

        public async Task UpdateStaffAsync(string StaffId, StaffDto staff)
        {
            await _httpClient.PatchAsJsonAsync($"{_apiUrl}/{StaffId}", staff);
        }

        public async Task DeleteStaffAsync(string StaffId)
        {
            await _httpClient.DeleteAsync($"{_apiUrl}/{StaffId}");
        }
    }
}
