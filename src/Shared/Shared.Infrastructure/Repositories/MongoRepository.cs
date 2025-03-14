using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Shared.Domain.Entities;
using Shared.Domain.Model;
using Shared.Domain.Repository;
using System.Linq.Expressions;
using System.Security.Authentication;

namespace Shared.Infrastructure.Repositories
{
    public partial class MongoRepository<TDocument> : IMongoRepository<TDocument> where TDocument : MongoDocument
    {
        private readonly IMongoCollection<TDocument> _collection;
        private readonly IMongoDatabase _database;
        private string? _databaseName = string.Empty; 
        private readonly string? _collectionName = string.Empty;

        //Mongo Atlas - encode password visit https://www.url-encode-decode.com/

        public MongoRepository(IOptions<MongoDbAppSettings> options)
        {
            //string? connectionString = _configuration["MongoDb:ConnectionString"];

            var settings = MongoClientSettings.FromConnectionString(options.Value.ConnectionString);

            settings.UseTls = false;
            settings.SslSettings = new SslSettings();
            settings.SslSettings.EnabledSslProtocols = SslProtocols.Tls12;

            // Set the ServerApi field of the settings object to set the version of the Stable API on the client
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            _collectionName = options.Value.CollectionName;
            _databaseName = options.Value.DatabaseName;

            _database = new MongoClient(settings).GetDatabase(_databaseName);
            _collection = _database.GetCollection<TDocument>(_collectionName);
        }

        public Task<IQueryable<TDocument>> AsQueryable() => Task.FromResult<IQueryable<TDocument>>(_collection.AsQueryable());
        public async Task<List<TDocument>> FilterByAsync(Expression<Func<TDocument, bool>> filterExpression) => await _collection.FindAsync(filterExpression).Result.ToListAsync();

        public async Task<List<TProjected>> FilterByAsync<TProjected>(
            Expression<Func<TDocument, bool>> filterExpression,
            Expression<Func<TDocument, TProjected>> projectionExpression)
        => await _collection.Find(filterExpression).Project(projectionExpression).ToListAsync();

        public async Task<TDocument> FindAsync(Expression<Func<TDocument, bool>> filterExpression)
        => await _collection.FindAsync<TDocument>(filterExpression).Result.FirstOrDefaultAsync();

        public async Task<TDocument> FindByIdAsync(string id)
        {
            ObjectId objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, objectId);
            return await _collection.FindAsync(filter).Result.FirstOrDefaultAsync();
        }

        public async Task InsertAsync(TDocument model)
        => await _collection.InsertOneAsync(model);

        public async Task InsertManyAsync(ICollection<TDocument> models)
        => await _collection.InsertManyAsync(models);

        public async Task UpdateAsync(TDocument model, UpdateDefinition<TDocument> updateDefenition)
        {
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, model.Id);
            await _collection.UpdateOneAsync(filter, updateDefenition);
        }

        public async Task ReplaceOneAsync(TDocument model)
        {
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, model.Id);
            await _collection.ReplaceOneAsync(filter, model);
        }

        public Task ReplaceManyAsync(List<TDocument> models)
        {
            models.ForEach(async model =>
               await _collection.ReplaceOneAsync(Builders<TDocument>.Filter.Eq(x => x.Id, model.Id), model));
            return Task.CompletedTask;
        }

        public async Task DeleteAsync(Expression<Func<TDocument, bool>> filterExpression)
        => await _collection.FindOneAndDeleteAsync(filterExpression);

        public async Task DeleteAsync(TDocument model)
        {
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, model.Id);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task DeleteByIdAsync(string id)
        {
            ObjectId objectId = new ObjectId(id);
            var filter = Builders<TDocument>.Filter.Eq(x => x.Id, objectId);
            await _collection.DeleteOneAsync(filter);
        }

        public async Task DeleteManyAsync(Expression<Func<TDocument, bool>> filterExpression)
        => await _collection.DeleteManyAsync(filterExpression);

        public async Task DeleteManyAsync(List<TDocument> models)
        {
            var filter = Builders<TDocument>.Filter.Where(x => models.Contains(x));
            await _collection.DeleteManyAsync(filter);
        }

        public async Task<bool> IsExist(Expression<Func<TDocument, bool>> filterExpression)
          => await _collection.AsQueryable().AnyAsync(filterExpression);

        public async Task<bool> IsExist(string id)
          => await _collection.AsQueryable().AnyAsync(x => x.Id.Equals(id));

        public void DropCollection()
        => _database.DropCollection(_collectionName);

        public async Task RenameCollectionAsync(string newName)
          => await _database.RenameCollectionAsync(_collectionName, newName);

        public async Task DropAllIndexesAsync()
          => await _collection.Indexes.DropAllAsync();

        public async Task DropIndexAsync(string indexName)
          => await _collection.Indexes.DropOneAsync(indexName);

        public async Task CreateIndexAsync(string indexName, CreateIndexOptions? options = null)
          => await _collection.Indexes.CreateOneAsync(indexName, options);

        public async Task CreateIndexesAsync(IEnumerable<CreateIndexModel<TDocument>> models)
        => await _collection.Indexes.CreateManyAsync(models);

        public async Task CreateIndexesAsync(Dictionary<string, CreateIndexOptions<TDocument>> keyOptions)
        {
            List<CreateIndexModel<TDocument>> models = new();
            foreach (var keyOption in keyOptions)
            {
                models.Add(new CreateIndexModel<TDocument>(keyOption.Key, keyOption.Value));
            }

            await _collection.Indexes.CreateManyAsync(models);
        }

        public async Task GetIndexListAsync(IEnumerable<string> keynames)
        => await _collection.Indexes.ListAsync();

        public async Task<long> CountAllDocumentsAsync()
          => await _collection.CountDocumentsAsync(_collectionName);

        public async Task<long> CountDocumentsAsync(Expression<Func<TDocument, bool>> filterExpression)
          => await _collection.CountDocumentsAsync(filterExpression);

        public Task<bool> PingMongo()
        {
            // Send a ping to confirm a successful connection
            try
            {
                var result = _database.RunCommand<BsonDocument>(new BsonDocument("ping", 1));
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }
    }
}