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
            .Where(a => a.UserId == ownerId && a.OwnerTypeId == (int)ownerType)
            .Include(a => a.AddressType)
            .ToListAsync();
        }

        public async Task<AddressEntity?> GetByOwnerAsync(int ownerId)
        {
            return await _context.Addresses
                .Include(a => a.AddressType)
                .FirstOrDefaultAsync(a =>
                    a.UserId == ownerId &&
                    a.IsDefault &&
                    a.IsActive);
        }

    }
}