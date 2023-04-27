namespace Tetris;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);
    }
}