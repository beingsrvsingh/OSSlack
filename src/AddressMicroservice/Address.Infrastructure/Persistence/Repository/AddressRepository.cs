using Address.Domain.Core.Repositories;
using Address.Domain.Entities;
using Address.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Address.Infrastructure.Persistence.Repository
{
    public class AddressRepository : Repository<AddressEntity>, IAddressRepository
    {
        private readonly AddressDbContext _context;

        public AddressRepository(AddressDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<AddressEntity>> GetAllByOwnerAsync(int ownerId, AddressOwnerType ownerType)
        {
            return await _context.Addresses
            .Where(a => a.OwnerId == ownerId && a.OwnerType == ownerType)
            .ToListAsync();
        }

        public async Task<AddressEntity?> GetByOwnerAsync(int ownerId)
        {
            return await _context.Addresses
                .FirstOrDefaultAsync(a =>
                    a.OwnerId == ownerId &&
                    a.IsDefault &&
                    a.IsActive);
        }

    }
}