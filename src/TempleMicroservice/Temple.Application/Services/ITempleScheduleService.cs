using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITempleScheduleService
    {
        Task<bool> CreateAsync(TempleSchedule schedule);
        Task<bool> UpdateAsync(TempleSchedule schedule);
        Task<bool> DeleteAsync(int id);
        Task<TempleSchedule?> GetByIdAsync(int id);
        Task<IEnumerable<TempleSchedule>> GetAllAsync();
    }

}
