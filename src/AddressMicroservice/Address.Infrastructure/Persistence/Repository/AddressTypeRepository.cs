using Address.Domain.Core.Repositories;
using Address.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Address.Infrastructure.Persistence.Repository
{
    public class AddressTypeRepository: Repository<AddressType>, IAddressTypeRepository
    {
        private readonly AddressDbContext _context;

        public AddressTypeRepository(AddressDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }
    }
}