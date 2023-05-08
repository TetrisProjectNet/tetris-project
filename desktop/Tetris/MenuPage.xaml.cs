using Mopups.Interfaces;
using Mopups.Pages;
using Mopups.Services;
using SharpHook;
using System.Linq;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text.Json;
using Tetris.Models;

namespace Tetris;

public partial class MenuPage : ContentPage
{
    private User _user;
    public MenuPage()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);
        GetUserData();

        if (DeviceInfo.Platform == DevicePlatform.Android) {
            TetrisLabel.FontSize = 80;
        }
    }

    private async void NewGameButtonClicked(object sender, TappedEventArgs e)
    {
        Thread.Sleep(250);
        await Navigation.PushAsync(new GamePage(), false);
    }

    private void StatisticsButtonClicked(object sender, TappedEventArgs e)
    {
        var uri = new Uri("https://tetris-project-net-2.web.app/statistics");
        Launcher.OpenAsync(uri);
    }

    private void ShopButtonClicked(object sender, TappedEventArgs e)
    {
        MopupService.Instance.PushAsync(new ShopPopupPage(_user));
    }

    private void SettingsButtonClicked(object sender, TappedEventArgs e)
    {

    }

    private void ProfileButtonClicked(object sender, TappedEventArgs e)
    {

    }

    private void QuitButtonClicked(object sender, TappedEventArgs e) {
        Application.Current.Quit();
    }

    private protected async Task GetUserData()
    {
        HttpClient httpClient = new HttpClient();
        string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", oauthToken);

        var response = await httpClient.GetAsync("https://localhost:7041/Auth");

        if (response.IsSuccessStatusCode)
        {
            var authContent = await response.Content.ReadAsStringAsync();

            _user = JsonSerializer.Deserialize<User>(authContent);
            Label coinsLabel = (Label)FindByName("coinsLabel");
            Label usernameLabel = (Label)FindByName("usernameLabel");
            coinsLabel.Text = _user.Coins.ToString();
            usernameLabel.Text = _user.Username;
        }
    }

    private void HoverBeganMenuButton(object sender, PointerEventArgs e) {
        ((Frame)sender).HeightRequest += 10;
        ((Frame)sender).WidthRequest += 50;

        var animation = new Animation {
            { 0, 1, new Animation(v => ((Frame)sender).BackgroundColor = Color.FromRgba("ff000080"), start: 0, end: 10000) },
            { 0, 1, new Animation(v => ((Frame)sender).TranslationX = v, 0, -10) }
        };

        animation.Commit(((Frame)sender), "AnimateBackgroundColor", length: 250);
    }

    private void HoverEndedMenuButton(object sender, PointerEventArgs e) {
        ((Frame)sender).HeightRequest -= 10;
        ((Frame)sender).WidthRequest -= 50;

        var animation = new Animation {
        { 0, 1, new Animation(v => ((Frame)sender).BackgroundColor = Color.FromRgba("2e2d30"), 0.3, 0) },
        { 0, 1, new Animation(v => ((Frame)sender).TranslationX = v, -10, 0) }
    };

        animation.Commit(((Frame)sender), "AnimateBackgroundColor", length: 250);
    }
}