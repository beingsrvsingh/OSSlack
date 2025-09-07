using Temple.Domain.Entities;

namespace Temple.Application.Services
{
    public interface ITempleDonationService
    {
        Task<bool> CreateAsync(TempleDonation donation);
        Task<bool> UpdateAsync(TempleDonation donation);
        Task<bool> DeleteAsync(int id);
        Task<TempleDonation?> GetByIdAsync(int id);
        Task<IEnumerable<TempleDonation>> GetAllAsync();
    }

}
