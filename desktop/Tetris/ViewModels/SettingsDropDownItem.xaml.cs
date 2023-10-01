using SkiaSharp;

namespace Tetris.ViewModels;

public partial class SettingsDropDownItem : ContentView
{
    private readonly SettingsCardView _cardView;
	public SettingsDropDownItem(string name, SettingsCardView cardView)
	{
		InitializeComponent();
        NameLabel.Text = name;
        _cardView = cardView;
	}

    private void PointerGestureRecognizer_PointerEntered(object sender, PointerEventArgs e) {
        NameLabel.TextColor = Colors.Red;
        MenuItemBorder.BackgroundColor = Color.FromRgba("131313");
    }

    private void PointerGestureRecognizer_PointerExited(object sender, PointerEventArgs e) {
        NameLabel.TextColor = Colors.White;
        MenuItemBorder.BackgroundColor = Color.FromRgba("161616");
    }

    private void TapCommand(object sender, EventArgs e) {
        _cardView.MenuItemSelectEvent(NameLabel.Text);
    }
}