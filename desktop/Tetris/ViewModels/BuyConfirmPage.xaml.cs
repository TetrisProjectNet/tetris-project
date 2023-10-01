using Mopups.Services;

namespace Tetris.ViewModels;

public partial class BuyConfirmPage
{
	private bool _buyConfirm;
    public event EventHandler<bool> BuyConfirmChanged;

    public BuyConfirmPage()
	{
		InitializeComponent();
	}

    private async void ConfirmPurchase(object sender, TappedEventArgs e) {
        _buyConfirm = true;
        CloseWindow();
        await Task.Delay(400);
        BuyConfirmChanged?.Invoke(this, _buyConfirm);
    }

    private async void DeclinePurchase(object sender, TappedEventArgs e) {
        _buyConfirm = false;
        CloseWindow();
        await Task.Delay(400);
        BuyConfirmChanged?.Invoke(this, _buyConfirm);
    }

    private void CloseWindow() {
        MopupService.Instance.PopAsync();
    }
}