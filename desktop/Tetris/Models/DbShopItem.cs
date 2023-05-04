using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models {
    public class DbShopItem {
        public string Name { get; set; }
        public string Image { get; set; }
        public DbShopItem(string name, string image) {
            Name = name;
            Image = image;
        }
    }
}
