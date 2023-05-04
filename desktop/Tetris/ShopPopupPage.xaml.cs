using Mopups.Services;

namespace Tetris;

public partial class ShopPopupPage
{
	public ShopPopupPage()
	{
		InitializeComponent();
	}

	private void BackButtonClicked(object sender, EventArgs e) {
		MopupService.Instance.PopAllAsync();
	}
}