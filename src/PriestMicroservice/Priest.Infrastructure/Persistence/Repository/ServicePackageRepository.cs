using Microsoft.EntityFrameworkCore;
using Priest.Domain.Core.Repository;
using Priest.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class ServicePackageRepository : Repository<ServicePackage>, IServicePackageRepository
    {
        private readonly DbContext dbContext;

        public ServicePackageRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }

}
