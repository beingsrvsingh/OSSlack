
using AstrologerMicroservice.Domain.Entities;

namespace AstrologerMicroservice.Application.Service
{
    public interface IScheduleService
{
    Task<Schedule?> GetByIdAsync(int id);
    Task<bool> CreateAsync(Schedule schedule);
    Task<bool> UpdateAsync(Schedule schedule);
    Task<bool> DeleteAsync(int id);
}
}