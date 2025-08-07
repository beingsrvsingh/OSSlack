using Microsoft.Extensions.Options;
using Shared.Domain.Entities;
using Shared.Domain.Model;
using Shared.Domain.Repository;
using Shared.Infrastructure.Repositories;

namespace Catalog.Infrastructure.Repositories
{
    public interface ISampleMongoRepository : IMongoRepository<MongoModel> { }

    public class SampleMongoRepository : MongoRepository<MongoModel>, ISampleMongoRepository
    {
        public SampleMongoRepository(IOptions<MongoDbAppSettings> options) : base(options)
        {
        }
    }

    public class MongoModel : BaseMongoDocument
    {
        public string name { get; set; } = null!;
    }  
}