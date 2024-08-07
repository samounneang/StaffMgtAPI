using StaffMgtBlazorApp.Models;

namespace StaffMgtBlazorApp.Services
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffDto>> GetStaffsAsync();
        Task<StaffDto> GetStaffAsync(string id);
        Task AddStaffAsync(StaffDto staff);
        Task UpdateStaffAsync(string id, StaffDto staff);
        Task DeleteStaffAsync(string id);
    }
}
