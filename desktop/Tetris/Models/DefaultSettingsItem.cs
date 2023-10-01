using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Models {
    public class DefaultSettingsItem {
        public string Name { get; set; }
        public string Ending { get; set; }
        public Color Color { get; set; }
        public string Image { get; set; }
        public string ArrowImage { get; set; }

        public DefaultSettingsItem(string name, string ending, Color color, string image, string arrowImage) {
            Name = name;
            Ending = ending;
            Color = color;
            Image = image;
            ArrowImage = arrowImage;
        }
    }
}
