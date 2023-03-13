using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace backend.Models;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("Name")]
    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Role { get; set; } = null!;

    public bool Banned { get; set; }

    public DateTime JoinDate { get; set; }

    public DateTime LastOnlineDate { get; set; }

    public int Coins { get; set; }

    public string[] ShopItems { get; set; } = null!;

    public int[] Scores { get; set; } = null!;

    public string[] Friends { get; set; } = null!;

}