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

namespace Tetris.ViewModels {
    public partial class CardViewModel : ObservableObject {

        [ObservableProperty]
        int row;

        [ObservableProperty]
        int column;

        [ObservableProperty]
        string name;

        [ObservableProperty]
        string imageString;

        [ObservableProperty]
        Color rectColor1;

        [ObservableProperty]
        Color rectColor3;

        [ObservableProperty]
        Color rectColor4;

        [ObservableProperty]
        int coinCount;

        [ObservableProperty]
        Thickness imageMargin = new Thickness(50, 150, 0, 0);

        [ObservableProperty]
        bool layered;

        public CardViewModel(int row, int column, string name, string imageString, Color rectColor1, Color rectColor3, Color rectColor4, int coinCount, bool layered) {
            this.row = row;
            this.column = column;
            this.name = name;
            this.imageString = imageString;
            this.rectColor1 = rectColor1;
            this.rectColor3 = rectColor3;
            this.rectColor4 = rectColor4;
            this.coinCount = coinCount;
            this.layered = layered;

            if (imageString.Contains("I.png")) this.imageMargin = new Thickness(0, 150, 0, 0);
            if (imageString.Contains("O.png")) this.imageMargin = new Thickness(20, 150, 0, 0);
        }

        [RelayCommand]
        async Task Tap() {
            if (!Layered) {
                await MopupService.Instance.PushAsync(new CardPopupPage(Name, ImageString, RectColor1, RectColor3, RectColor4, CoinCount));
                return;
            }
            HandleBuyEvent();
        }

        public void HandleBuyEvent() {

        }
    }
}
