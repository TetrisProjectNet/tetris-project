using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Models;

namespace Tetris.References {
    internal class ShopItemIdReferences {
        public static readonly DbShopItem[] _ShopImageReferences = new DbShopItem[] {
            new("Cloudy", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullcloudy.png")),
            new("Foxy", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullfox.png")),
            new("Minecraft", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullminecraft.png")),
            new("Peach", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullpeach.png")),
            new("Shiny Dark Blue", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinydarkblue.png")),
            new("Shiny Green", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinygreen.png")),
            new("Shiny Light Blue", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinylightblue.png")),
            new("Shiny Orange", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinyorange.png")),
            new("Shiny Ruby", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinyruby.png")),
            new("Shiny Purple", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinyviolet.png")),
            new("Shiny Yellow", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinyyellow.png")),
            new("Yellow Boy Crown", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullyellowboycrown.png")),
            new("Yellow Girl Crown", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullyellowgirlcrown.png")),
            new("Yellow X", Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullyellowx.png")),
        };

        public static readonly DefaultSettingsItem[] defaultSettingItems = new DefaultSettingsItem[] {
            new("Red", "Z.png", Color.FromRgba("ff0000"), "fullred.png", "settingsredarrow.png"),
            new("Orange", "L.png", Color.FromRgba("ff7f00"), "fullorange.png", "settingsorangearrow.png"),
            new("Yellow", "O.png", Color.FromRgba("ffff00"), "fullyellow.png", "settingsyellowarrow.png"),
            new("Green", "S.png", Color.FromRgba("00ff00"), "fullgreen.png", "settingsgreenarrow.png"),
            new("Light Blue", "I.png", Color.FromRgba("00ffff"), "fulllightblue.png", "settingslightbluearrow.png"),
            new("Dark Blue", "J.png", Color.FromRgba("0000ff"), "fulldarkblue.png", "settingsdarkbluearrow.png"),
            new("Purple", "T.png", Color.FromRgba("800080"), "fullpurple.png", "settingspurplearrow.png"),
        };

        public static readonly string[] TilePaths = new string[] {
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\cloudytile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\foxtile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\minecrafttile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\peachtile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\shinydarkbluetile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\shinygreentile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\shinylightbluetile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\shinyorangetile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\shinyrubytile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\shinyviolettile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\shinyyellowtile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\yellowboycrowntile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\yellowgirlcrowntile.png"),
            Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\yellowxtile.png"),
        };

        public static readonly int[] SettingsIdToTile = new int[] {
            3, 4, 1, 6, 0, 2, 5
         // S, I, L, T, Z, O, J
        };
    }
}
