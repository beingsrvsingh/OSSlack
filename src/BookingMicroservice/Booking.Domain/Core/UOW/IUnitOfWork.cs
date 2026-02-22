using BookingMicroservice.Domain.Repositories;
using Shared.Domain.Common.Entities.Interface;
using Shared.Domain.UOW;

namespace BookingMicroservice.Domain.UOW
{
    public interface IUnitOfWork : IBaseUnitOfWork, IAuditLog, IDisposable
    {
        IBookingRepository Astrologers { get; }
    }
}