using Kathavachak.Domain.Entities;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakScheduleService
    {
        Task<IEnumerable<Schedule>> GetAllAsync();
        Task<Schedule?> GetByIdAsync(int id);
        Task<bool> CreateAsync(Schedule entity);
        Task<bool> UpdateAsync(Schedule entity);
        Task<bool> DeleteAsync(int id);
    }

}
