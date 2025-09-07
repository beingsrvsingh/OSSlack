using Kathavachak.Domain.Entities;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakScheduleService
    {
        Task<IEnumerable<KathavachakSchedule>> GetAllAsync();
        Task<KathavachakSchedule?> GetByIdAsync(int id);
        Task<bool> CreateAsync(KathavachakSchedule entity);
        Task<bool> UpdateAsync(KathavachakSchedule entity);
        Task<bool> DeleteAsync(int id);
    }

}
