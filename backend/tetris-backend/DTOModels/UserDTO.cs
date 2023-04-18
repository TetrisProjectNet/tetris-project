using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace tetris_backend.DTOModels
{
    public class UserDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool Banned { get; set; } = false!;
        public string JoinDate { get; set; } = null!;
        public string? LastOnlineDate { get; set; } = null!;
        public int Coins { get; set; } = 0!;
        public ShopItem[]? ShopItems { get; set; } = null!;
        public int[]? Scores { get; set; } = null!;
        public string[]? Friends { get; set; } = null!;

    }
}
