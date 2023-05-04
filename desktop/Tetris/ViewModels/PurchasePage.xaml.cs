using Mopups.Services;
using SkiaSharp.Extended.UI.Controls;

namespace Tetris.ViewModels;

public partial class PurchasePage
{
    private bool _purchaseValid;
    private static readonly string[] _popupMessages = new string[] {
        "Thank you for your purchase !",
        "You can use this skin from now on !",
        "You don't have enough coins !",
    };
	public PurchasePage(bool purchaseValid)
	{
		InitializeComponent();
        _purchaseValid = purchaseValid;

		PlayLoadingAnimation();
	}

    private async void PlayLoadingAnimation() {
        PurchaseAnimation.IsVisible = false;
        PurchaseAnimation.IsAnimationEnabled = !PurchaseAnimation.IsAnimationEnabled;
        await Task.Delay(100);
        PurchaseAnimation.IsAnimationEnabled = !PurchaseAnimation.IsAnimationEnabled;
        PurchaseAnimation.IsVisible = true;
        if (!_purchaseValid) {
            var ts = TimeSpan.FromMilliseconds(7000);
            PurchaseAnimation.Progress = ts;
        }
        PurchaseAnimation.IsAnimationEnabled = !PurchaseAnimation.IsAnimationEnabled;
        await Task.Delay(5000);
        Task.Run(DisablePurchaseAnimation);

		MoveAnimationUpwards();
        ExpandBackgroundBorder();
        await Task.Delay(500);
        ExpandTitleBorder();
        await Task.Run(HandlePopupMessage);
    }

    private async void DisablePurchaseAnimation() {
        await Task.Delay(1500);
        MainThread.BeginInvokeOnMainThread(() => {
            PurchaseAnimation.IsAnimationEnabled = !PurchaseAnimation.IsAnimationEnabled;
        });
    }

	private void MoveAnimationUpwards() {
        var animation = new Animation {
            { 0, 1, new Animation(v => PurchaseAnimation.Margin = new Thickness(0, v, 0, 0), 0, -100) },
            { 0, 1, new Animation(v => PurchaseAnimation.HeightRequest = v, PurchaseAnimation.HeightRequest, 70) },
        };

        animation.Commit(PurchaseAnimation, "MovePurchaseAnimation", length: 400);
    }

    private void ExpandBackgroundBorder() {
        PurchaseTextBorder.IsVisible = true;
        var animation = new Animation {
            { 0, 1, new Animation(v => PurchaseTextBorder.WidthRequest = v, 0, 600) },
        };

        animation.Commit(PurchaseTextBorder, "ExpandBackgroundBorder", length: 500);
    }

    private void ExpandOkButton() {
        PurchaseOkBorder.IsVisible = true;
        var animation = new Animation {
            { 0, 1, new Animation(v => PurchaseOkBorder.WidthRequest = v, 0, 150) },
        };

        animation.Commit(PurchaseOkBorder, "ExpandOkButton", length: 500);
    }

    private void ExpandTitleBorder() {
        PurchaseTitleBorder.IsVisible = true;
        var animation = new Animation {
            { 0, 1, new Animation(v => PurchaseTitleBorder.WidthRequest = v, 0, 200) },
        };

        animation.Commit(PurchaseTitleBorder, "ExpandTitleBorder", length: 400);
    }

    private async void HandlePopupMessage() {
        int specifiedRowToWrite = 0;
        if (!_purchaseValid) specifiedRowToWrite += 2;

        MainThread.BeginInvokeOnMainThread(async () => {
            await WriteWaitingWriteToMessage(PopupMessage1);
            await WritePopupMessage(specifiedRowToWrite, PopupMessage1);
            await Task.Delay(200);
            if (_purchaseValid) {
                //await WriteWaitingWriteToMessage(PopupMessage2);
                await WritePopupMessage(specifiedRowToWrite + 1, PopupMessage2);
            }

            ExpandOkButton();
        });
    }

    private async Task WritePopupMessage(int row, Label messageLabel) {
        for (int i = 0; i < _popupMessages[row].Length; i++) {
            messageLabel.Text += _popupMessages[row][i];
            await Task.Delay(50);
        }
    }

    private async Task WriteWaitingWriteToMessage(Label messageLabel) {
        for (int i = 0; i < 11; i++) {
            if (messageLabel.Text == ". . . ") {
                messageLabel.Text = "";
                await Task.Delay(200);
                continue;
            }
            messageLabel.Text += ". ";
            await Task.Delay(200);
        }
        messageLabel.Text = "";
        await Task.Delay(100);
    }

    private void OkButtonTapped(object sender, TappedEventArgs e) {
        MopupService.Instance.PopAsync();
    }
}