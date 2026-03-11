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

        public async Task<IEnumerable<AddressEntity>> GetAllByOwnerAsync(string userId, AddressOwnerType ownerType)
        {
            return await _context.Addresses
            .Where(a => a.UserId == userId && a.OwnerTypeId == (int)ownerType)
            .Include(a => a.AddressType)
            .ToListAsync();
        }

        public async Task<AddressEntity?> GetByOwnerAsync(string userId)
        {
            return await _context.Addresses
                .Include(a => a.AddressType)
                .FirstOrDefaultAsync(a =>
                    a.UserId == userId &&
                    a.IsDefault &&
                    a.IsActive);
        }

        public async new Task<AddressEntity?> GetByIdAsync(int id)
        {
            return await _context.Addresses
                .Include(a => a.AddressType)
                .FirstOrDefaultAsync(a =>
                    a.Id == id &&
                    a.IsActive);
        }

    }
}