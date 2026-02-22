using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITempleScheduleService
    {
        Task<bool> CreateAsync(Schedule schedule);
        Task<bool> UpdateAsync(Schedule schedule);
        Task<bool> DeleteAsync(int id);
        Task<Schedule?> GetByIdAsync(int id);
        Task<IEnumerable<Schedule>> GetAllAsync();
    }

}
