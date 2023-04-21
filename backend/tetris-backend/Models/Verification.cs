using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace tetris_backend.Models
{
    public class Verification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("code")]
        public string? Code { get; set; } = null!;

        //[BsonElement("createdAt")]
        //public string? CreatedAt { get; set; } = null!;

        [BsonElement("expireAt")]
        public DateTime ExpireAt { get; set; }


    }

    //var indexKeysDefinition = Builders<MongoLogMessage>.IndexKeys.Ascending("ExpireAt");
    //var indexOptions = new CreateIndexOptions { **ExpireAfter = new TimeSpan(0, 0, 0) * * };
    //var indexModel = new CreateIndexModel<MongoLogMessage>(indexKeysDefinition, indexOptions);
    //_mongoCollection.Indexes.CreateOne(indexModel);
}
