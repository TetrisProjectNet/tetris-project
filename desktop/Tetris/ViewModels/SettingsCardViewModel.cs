using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mopups.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tetris.Models;
using Tetris.References;

namespace Tetris.ViewModels {
    public partial class SettingsCardViewModel : ObservableObject {

        public int _colNumber;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string imageString;

        [ObservableProperty]
        string arrowImage;

        [ObservableProperty]
        Color rectColor;

        [ObservableProperty]
        Thickness imageMargin = new Thickness(40, 150, 0, 0);

        [ObservableProperty]
        List<string> skinNames;

        public int[][] skinsUsed;

        public SettingsCardViewModel(string name, string imageString, string arrowImage, Color rectColor, List<string> skinNames, int colNumber) {
            this.name = name;
            this.imageString = imageString;
            this.arrowImage = arrowImage;
            this.rectColor = rectColor;
            this.skinNames = skinNames;
            _colNumber = colNumber;

            if (imageString.Contains("I.png") || imageString.Contains("lightblue.png") || imageString.Contains("O.png") || imageString.Contains("yellow.png")) this.imageMargin = new Thickness(0, 150, 0, 0);

            int usedSkin = Preferences.Default.Get($"skinSlot{_colNumber}", -1);
            if (usedSkin != -1) SwitchToAlreadySelectedSkin(usedSkin);
        }

        internal void HandleSelectedSkin(string name) {
            if (name == "Reset") {
                Name = ShopItemIdReferences.defaultSettingItems[_colNumber].Name;
                ImageString = ShopItemIdReferences.defaultSettingItems[_colNumber].Image;
                Preferences.Default.Set($"skinSlot{_colNumber}", - 1);
                return;
            }
            Name = name;
            var index = Array.FindIndex(ShopItemIdReferences._ShopImageReferences, x => x.Name == name);
            Preferences.Default.Set($"skinSlot{_colNumber}", index);

            var image = ShopItemIdReferences._ShopImageReferences[index].Image;
            ImageString = image.Replace(".png", ShopItemIdReferences.defaultSettingItems[_colNumber].Ending);
        }

        private void SwitchToAlreadySelectedSkin(int skinId) {
            Name = ShopItemIdReferences._ShopImageReferences[skinId].Name;
            var image = ShopItemIdReferences._ShopImageReferences[skinId].Image;
            ImageString = image.Replace(".png", ShopItemIdReferences.defaultSettingItems[_colNumber].Ending);
        }
    }
}
