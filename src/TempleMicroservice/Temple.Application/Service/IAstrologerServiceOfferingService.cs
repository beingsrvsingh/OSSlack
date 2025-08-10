using Temple.Domain.Entities;

namespace Temple.Application.Service
{
    public interface IAstrologerServiceOfferingService
    {
        Task<IEnumerable<ServicePackage>> GetByAstrologerIdAsync(int astrologerId);
        Task<ServicePackage?> GetByIdAsync(int serviceOfferingId);

        Task<ServicePackage> CreateAsync(ServicePackage offering);
        Task<bool> UpdateAsync(ServicePackage offering);
        Task<bool> DeleteAsync(int serviceOfferingId);

        Task<bool> SetServiceOfferingsAsync(int astrologerId, IEnumerable<ServicePackage> offerings);
    }
}