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

        public string? username { get; set; } = null!;

        public byte[]? passwordHash { get; set; } = null!;

        public byte[]? passwordSalt { get; set; } = null!;

        public string? email { get; set; } = null!;

        public string? role { get; set; } = null!;

        public bool? banned { get; set; } = false!;

        public string? joinDate { get; set; } = null!;

        public string? lastOnlineDate { get; set; } = null!;

        public int? coins { get; set; } = 0!;

        public string[]? shopItems { get; set; } = null!;

        public int[]? scores { get; set; } = null!;

        public string[]? friends { get; set; } = null!;

        public string? refreshToken { get; set; } = string.Empty;

        public DateTime? tokenCreated { get; set; }

        public DateTime? tokenExpires { get; set; }
    }
}
