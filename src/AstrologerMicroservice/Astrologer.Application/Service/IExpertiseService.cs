using AstrologerMicroservice.Domain.Entities;

namespace AstrologerMicroservice.Application.Service
{
    public interface IExpertiseService
    {
        Task<IEnumerable<AstrologerExpertise>> GetAllAsync();
        Task<AstrologerExpertise?> GetByIdAsync(int id);
        Task<AstrologerExpertise> CreateAsync(AstrologerExpertise expertise);
        Task<bool> UpdateAsync(AstrologerExpertise expertise);
        Task<bool> DeleteAsync(int id);
    }
}