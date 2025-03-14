using Catalog.Domain.Core.Repository;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Microsoft.Extensions.Options;
using Shared.Domain.Entities;
using Shared.Domain.Model;
using Shared.Domain.Repository;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public class CatalogRepository : Repository<CategoryMaster>, ICatalogRepository
    {
        private readonly CatalogDbContext dbContext;

        public CatalogRepository(CatalogDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }

    public interface ISampleMongoRepository : IMongoRepository<MongoModel> { }

    public class SampleMongoRepository : MongoRepository<MongoModel>, ISampleMongoRepository
    {
        public SampleMongoRepository(IOptions<MongoDbAppSettings> options) : base(options)
        {
        }
    }

    public class MongoModel : MongoDocument
    {
        public string name { get; set; } = null!;
    }    
}
