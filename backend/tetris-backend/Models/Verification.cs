using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

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


    }
}
