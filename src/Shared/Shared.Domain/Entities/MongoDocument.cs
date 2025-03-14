using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Shared.Domain.Entities
{
    public abstract class MongoDocument
    {
        [BsonId]
        [BsonRequired]
        [BsonRepresentation(BsonType.ObjectId)]
        public virtual ObjectId Id { get; set; }
        public long CreateDate { get; set; }
        public long CreateBy { get; set; }
        public long? DeleteDate { get; set; }
        public long? DeleteBy { get; set; }

        public MongoDocument()
        {
            CreateDate = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        }

    }
}
