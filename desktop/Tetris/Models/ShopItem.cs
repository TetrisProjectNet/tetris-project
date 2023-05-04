using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Tetris.Models {
    public class ShopItem {
        [JsonPropertyName("id")]
        public string Id { get; private set; }
        
        [JsonPropertyName("index")]
        public int Index { get; private set; }

        [JsonPropertyName("title")]
        public string Title { get; private set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }

        [JsonPropertyName("price")]
        public int Price { get; private set; }

        [JsonPropertyName("color")]
        public string Color { get; private set;  }

        [JsonPropertyName("description")]
        public string Description { get; private set; }

        public ShopItem(string id, int index, string title, string image, int price, string color, string description) {
            Id = id;
            Index = index;
            Title = title;
            Image = image;
            Price = price;
            Color = color;
            Description = description;
        }
    }
}
