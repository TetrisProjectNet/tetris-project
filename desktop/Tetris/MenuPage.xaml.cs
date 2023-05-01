using SharpHook;
using System.Net.Http.Headers;

namespace Tetris;

public partial class MenuPage : ContentPage
{
	public MenuPage(string token)
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);
        GetUserData(token);
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

    private protected async Task GetUserData(string token)
    {
        HttpClient httpClient = new HttpClient();

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var response = await httpClient.GetAsync("https://localhost:7041/Auth");

        if (response.IsSuccessStatusCode)
        {
            var authContent = await response.Content.ReadAsStringAsync();

            var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(authContent);
            string coins = keyValuePairs["coins"].ToString();
            string name = keyValuePairs["username"].ToString();
            Label coinsLabel = (Label)FindByName("coinsLabel");
            Label usernameLabel = (Label)FindByName("usernameLabel");
            coinsLabel.Text = coins;
            usernameLabel.Text = name;
        }
    }
}