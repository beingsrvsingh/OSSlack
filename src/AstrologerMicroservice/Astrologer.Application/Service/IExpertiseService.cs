using AstrologerMicroservice.Domain.Entities;

namespace AstrologerMicroservice.Application.Service
{
    public interface IExpertiseService
    {
        Task<IEnumerable<Expertise>> GetAllAsync();
        Task<Expertise?> GetByIdAsync(int id);
        Task<Expertise> CreateAsync(Expertise expertise);
        Task<bool> UpdateAsync(Expertise expertise);
        Task<bool> DeleteAsync(int id);
    }
}