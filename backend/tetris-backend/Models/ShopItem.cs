using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace tetris_backend.Models
{
    public class ShopItem
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("index")]
        public int Index { get; set; } = 0!;

        [BsonElement("title")]
        public string Title { get; set; } = null!;

        [BsonElement("image")]
        public string? Image { get; set; } = null!;

        [BsonElement("price")]
        public int Price { get; set; } = 0!;

        [BsonElement("color")]
        public string? Color { get; set; } = null!;

        [BsonElement("description")]
        public string? Description { get; set; } = null!;

    }
}
