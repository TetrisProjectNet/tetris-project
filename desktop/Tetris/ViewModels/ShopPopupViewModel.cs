using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tetris.Models;

namespace Tetris.ViewModels {
    public class ShopPopupViewModel {
        private ShopItem[] shopItems;
        private ShopItem[] originShopItems;

        public ShopItem[] ShopItems {
            get => shopItems;
            set => shopItems = value;
        }

        public ShopItem[] OriginShopItems {
            get => originShopItems;
            set => originShopItems = value;
        }
    }
}
