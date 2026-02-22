using BookingMicroservice.Domain.Entities;
using BookingMicroservice.Domain.Repositories;
using BookingMicroservice.Domain.UOW;
using BookingMicroservice.Infrastructure.Persistence.Context;
using Shared.Domain.Entities;
using Shared.Infrastructur.UoW;

namespace BookingMicroservice.Infrastructure.UOW
{
    public class UnitOfWork : BaseUnitOfWork<BookingDbContext, AuditLog>, IUnitOfWork
    {
        public UnitOfWork(BookingDbContext dbContext,
        IBookingRepository bookingRepo) : base(dbContext)
        {
            Astrologers = bookingRepo;
        }

        public IBookingRepository Astrologers { get; }


        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}