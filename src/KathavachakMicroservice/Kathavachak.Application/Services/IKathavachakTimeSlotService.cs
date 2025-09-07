using Kathavachak.Domain.Entities;

namespace Kathavachak.Application.Services
{
    public interface IKathavachakTimeSlotService
    {
        Task<IEnumerable<KathavachakTimeSlot>> GetAllAsync();
        Task<KathavachakTimeSlot?> GetByIdAsync(int id);
        Task<bool> CreateAsync(KathavachakTimeSlot entity);
        Task<bool> UpdateAsync(KathavachakTimeSlot entity);
        Task<bool> DeleteAsync(int id);
    }
}
