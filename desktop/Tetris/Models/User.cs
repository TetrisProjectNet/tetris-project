using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tetris.Models {
    public class User {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("banned")]
        public bool Banned { get; set; }

        [JsonPropertyName("joinDate")]
        public string? JoinDate { get; set; }

        [JsonPropertyName("lastOnlineDate")]
        public string? LastOnlineDate { get; set; }

        [JsonPropertyName("coins")]
        public int Coins { get; set; }

        [JsonPropertyName("shopItems")]
        public ShopItem[]? ShopItems { get; set; }

        [JsonPropertyName("scores")]
        public int[]? Scores { get; set; }

        [JsonPropertyName("friends")]
        public int[]? Friends { get; set; }

        public User(string id, string username, string password, string email, string role, bool banned, string joinDate,
                    string lastOnlineDate, int coins, ShopItem[] shopItems, int[] scores, int[] friends) {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            Role = role;
            Banned = banned;
            JoinDate = joinDate;
            LastOnlineDate = lastOnlineDate;
            Coins = coins;
            ShopItems = shopItems;
            Scores = scores;
            Friends = friends;
        }
    }
}
