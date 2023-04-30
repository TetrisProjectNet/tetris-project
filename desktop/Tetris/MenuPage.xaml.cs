namespace Tetris;

public partial class MenuPage : ContentPage
{
	public MenuPage()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private async void NewGameButtonClicked(object sender, TappedEventArgs e)
    {
        Thread.Sleep(250);
        await Navigation.PushAsync(new GamePage(), false);
    }

    private void StatisticsButtonClicked(object sender, TappedEventArgs e)
    {

    }

    private void ShopButtonClicked(object sender, TappedEventArgs e)
    {

    }

    private async void SettingsButtonClicked(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Error", "Not implemented yet!", "OK");
    }

    private async void ProfileButtonClicked(object sender, TappedEventArgs e)
    {
        await DisplayAlert("Error", "Not implemented yet!", "OK");
    }
}