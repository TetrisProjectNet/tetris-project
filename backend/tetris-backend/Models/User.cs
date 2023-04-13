using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace tetris_backend.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("role")]
        public string Role { get; set; } = null!;

        [BsonElement("banned")]
        public bool Banned { get; set; } = false!;

        [BsonElement("joinDate")]
        public string JoinDate { get; set; } = null!;

        [BsonElement("lastOnlineDate")]
        public string? LastOnlineDate { get; set; } = null!;

        [BsonElement("coins")]
        public int Coins { get; set; } = 0!;

        //[JsonIgnore]
        [BsonElement("shopItems")]
        public dynamic[]? ShopItems { get; set; } = null!;
        //public virtual ICollection<ShopItem>? ShopItems { get; set; } = null!;

        [BsonElement("scores")]
        public int[]? Scores { get; set; } = null!;

        [BsonElement("friends")]
        public string[]? Friends { get; set; } = null!;

    }
}
