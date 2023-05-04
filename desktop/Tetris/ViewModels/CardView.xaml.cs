using CommunityToolkit.Mvvm.Messaging;
using Mopups.Services;
using Tetris.Messages;

namespace Tetris.ViewModels;

public partial class CardView : ContentView, IRecipient<ItemBuyMessage> {
    private bool _swipeInAction;
    public CardViewModel cvm;
    private int swipeState = 6;
    private static readonly string[] swipeImageStrings = new string[] {
        "I.png", "J.png", "L.png", "O.png", "S.png", "T.png", "Z.png"
    };
    private bool _buyConfirm;
	public CardView(CardViewModel cvm)
	{
		InitializeComponent();
		BindingContext = cvm;
        this.cvm = cvm;
	}

    private void HoverBeganCard(object sender, PointerEventArgs e) {
        ((Border)sender).ScaleTo(1.1);
    }

    private void HoverEndedCard(object sender, PointerEventArgs e) {
        ((Border)sender).ScaleTo(1.0);
    }

    private void HoverBeganBuy(object sender, PointerEventArgs e) {
        if (cvm.bought) return;
        var animation = new Animation {
            { 0, 1, new Animation(v => ((StackLayout)sender).BackgroundColor = Color.FromRgba("308a3899"), start: 0, end: 10000) },
            { 0, 1, new Animation(v => ((StackLayout)sender).TranslationX = v, 0, -20) }
        };

        animation.Commit(((StackLayout)sender), "AnimateBackgroundColorAndTranslation", length: 250);
    }

    private void HoverEndedBuy(object sender, PointerEventArgs e) {
        if (cvm.bought) return;
        var animation = new Animation {
        { 0, 1, new Animation(v => ((StackLayout)sender).BackgroundColor = Color.FromRgba(48, 138, 56, v), 0.3, 0) },
        { 0, 1, new Animation(v => ((StackLayout)sender).TranslationX = v, -20, 0) }
    };

        animation.Commit(((StackLayout)sender), "AnimateBackgroundColorAndTranslation", length: 250);
    }

    private void BuyButtonTapEvent(object sender, TappedEventArgs e) {
        if (cvm.bought) return;
        var confirmPopup = new BuyConfirmPage();
        confirmPopup.BuyConfirmChanged += OnBuyConfirmChanged;

        MopupService.Instance.PushAsync(confirmPopup);
    }

    private void OnBuyConfirmChanged(object sender, bool buyConfirm) {
        _buyConfirm = buyConfirm;

        if (_buyConfirm) {

            CloseLayered();
            Task.Delay(700);
            WeakReferenceMessenger.Default.Send(new ItemBuyMessage(cvm.Name));
            //MopupService.Instance.PushAsync(new PurchasePage(true));
        }
    }

    private void CloseLayered() {
        if (cvm.Layered) MopupService.Instance.PopAsync();
    }

    public async void RightAnimation() {
        if (_swipeInAction) return;
        _swipeInAction = true;
        await Task.Run(FireRightSwipeAnimation);
        await Task.Delay(800);
        _swipeInAction = false;
    }

    public async void LeftAnimation() {
        if (_swipeInAction) return;
        _swipeInAction = true;
        await Task.Run(FireLeftSwipeAnimation);
        await Task.Delay(800);
        _swipeInAction = false;
    }

    public async Task FireRightSwipeAnimation() {
        var animation1 = new Animation {
                    { 0, 1, new Animation(v => this.TranslationX = v, start: 0, end: Window.Width) },
                };

        animation1.Commit(this, "PopupPageSlideOut", length: 300, finished: (d, b) => {
            try {
                var animation2 = new Animation {
                        { 0, 1, new Animation(v => this.TranslationX = v, start: -Window.Width, end: 0) },
                    };
                cvm.ImageString = cvm.ImageString.Replace(swipeImageStrings[swipeState], swipeImageStrings[swipeState + 1 == 7 ? 0 : swipeState + 1]);
                swipeState = swipeState + 1 == 7 ? 0 : swipeState + 1;
                animation2.Commit(this, "PopupPageSlideIn", length: 300);
            } catch (NullReferenceException e) {
                return;
            }
            if (cvm.ImageString.Contains("I.png")) {
                cvm.ImageMargin = new Thickness(0, 150, 0, 0);
                return;
            }
            if (cvm.ImageString.Contains("O.png")) {
                cvm.ImageMargin = new Thickness(20, 150, 0, 0);
                return;
            }
            cvm.ImageMargin = new Thickness(50, 150, 0, 0);
        });
    }

    public async Task FireLeftSwipeAnimation() {
        var animation1 = new Animation {
                { 0, 1, new Animation(v => this.TranslationX = v, start: 0, end: -Window.Width) },
            };

        animation1.Commit(this, "PopupPageSlideOut", length: 300, finished: (d, b) => {
            try {
                var animation2 = new Animation {
                        { 0, 1, new Animation(v => this.TranslationX = v, start: Window.Width, end: 0) },
                    };
                cvm.ImageString = cvm.ImageString.Replace(swipeImageStrings[swipeState], swipeImageStrings[swipeState - 1 == -1 ? 6 : swipeState - 1]);
                swipeState = swipeState - 1 == -1 ? 6 : swipeState - 1;
                animation2.Commit(this, "PopupPageSlideIn", length: 300);
            } catch (NullReferenceException e) {
                return;
            }
            if (cvm.ImageString.Contains("I.png")) {
                cvm.ImageMargin = new Thickness(0, 150, 0, 0);
                return;
            }
            if (cvm.ImageString.Contains("O.png")) {
                cvm.ImageMargin = new Thickness(20, 150, 0, 0);
                return;
            }
            cvm.ImageMargin = new Thickness(50, 150, 0, 0);
        });
    }

    public void Receive(ItemBuyMessage message) {
        throw new NotImplementedException();
    }
}