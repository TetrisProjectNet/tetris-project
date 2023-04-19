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
        public string? id { get; set; }

        //[BsonElement("username")]
        public string? username { get; set; } = null!;

        //[BsonElement("passwordHash")]
        public byte[]? passwordHash { get; set; } = null!;

        //[BsonElement("passwordSalt")]
        public byte[]? passwordSalt { get; set; } = null!;

        //[BsonElement("email")]
        public string? email { get; set; } = null!;

        //[BsonElement("role")]
        public string? role { get; set; } = null!;

        //[BsonElement("banned")]
        public bool? banned { get; set; } = false!;

        //[BsonElement("joinDate")]
        public string? joinDate { get; set; } = null!;

        //[BsonElement("lastOnlineDate")]
        public string? lastOnlineDate { get; set; } = null!;

        //[BsonElement("coins")]
        public int? coins { get; set; } = 0!;

        //[JsonIgnore]
        //[BsonElement("shopItems")]
        public string[]? shopItems { get; set; } = null!;
        //public virtual ICollection<ShopItem>? ShopItems { get; set; } = null!;

        //[BsonElement("scores")]
        public int[]? scores { get; set; } = null!;

        //[BsonElement("friends")]
        public string[]? friends { get; set; } = null!;

        public string? refreshToken { get; set; } = string.Empty;
        public DateTime? tokenCreated { get; set; }
        public DateTime? tokenExpires { get; set; }

    }
}
