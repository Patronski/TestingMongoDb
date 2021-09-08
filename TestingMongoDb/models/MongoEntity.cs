using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestingMongoDb.models
{
    public abstract class MongoEntity : EntityBase
    {
        [BsonIgnoreIfDefault]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
