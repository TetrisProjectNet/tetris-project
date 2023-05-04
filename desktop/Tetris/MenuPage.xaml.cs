using CommunityToolkit.Mvvm.Messaging;
using MetroLog;
using Microsoft.Extensions.Logging;
using Mopups.Interfaces;
using Mopups.Pages;
using Mopups.Services;
using Plugin.Maui.Audio;
using SharpHook;
using System.Linq;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text.Json;
using Tetris.Messages;
using Tetris.Models;
using Tetris.ViewModels;

namespace Tetris;

public partial class MenuPage : ContentPage, IRecipient<ItemBuyPriceMessage> {
    private User _user;
    private IPopupNavigation _popupNavigation;
    private readonly IAudioManager _audioManager;
    private IAudioPlayer _audioplayer;
    public MenuPage(IAudioManager audioManager, IPopupNavigation popupNavigation)
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);
        this._popupNavigation = popupNavigation;
        this._audioManager = audioManager;
        if (_audioplayer is null || !_audioplayer.IsPlaying) PlayBackgroundMusic();
        _ = GetUserData();

        if (DeviceInfo.Platform == DevicePlatform.Android) {
            TetrisLabel.FontSize = 80;
        }

        WeakReferenceMessenger.Default.Register<ItemBuyPriceMessage>(this);
    }
    public async void PlayBackgroundMusic() {
        var audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("menubackgroundmusic.mp3"));
        this._audioplayer = audioPlayer;
        audioPlayer.Play();
        audioPlayer.Volume = Preferences.Default.Get("musicvolume", 0.1);
        audioPlayer.Loop = true;
    }

    private async void NewGameButtonClicked(object sender, TappedEventArgs e)
    {
        PlayMenuSound("menubuttonclicked.wav", 400);
        await Task.Delay(500);
        _audioplayer.Stop();
        await Navigation.PushAsync(new GamePage(AudioManager.Current, this.Handler.MauiContext.Services.GetServices<ILogger<GamePage>>().FirstOrDefault()), false);
    }

    private void StatisticsButtonClicked(object sender, TappedEventArgs e)
    {
        PlayMenuSound("menubuttonclicked.wav", 400);
        var uri = new Uri("https://tetris-project-net-2.web.app/statistics");
        Launcher.OpenAsync(uri);
    }

    private void ShopButtonClicked(object sender, TappedEventArgs e)
    {
        PlayMenuSound("menubuttonclicked.wav", 400);
        ShopPopupViewModel vm = this.Handler.MauiContext.Services.GetServices<ShopPopupViewModel>().FirstOrDefault();
        _popupNavigation.PushAsync(new ShopPopupPage(_user, vm));
    }

    private void SettingsButtonClicked(object sender, TappedEventArgs e)
    {
        PlayMenuSound("menubuttonclicked.wav", 400);
        _popupNavigation.PushAsync(new SettingsPopupPage(_user, _audioplayer));
    }

    private void ProfileButtonClicked(object sender, TappedEventArgs e)
    {
        PlayMenuSound("menubuttonclicked.wav", 400);
    }

    private void QuitButtonClicked(object sender, TappedEventArgs e) {
        PlayMenuSound("menubuttonclicked.wav", 400);
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
        PlayMenuSound("menubuttonhover.wav", 150);
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
    protected override void OnAppearing() {
        base.OnAppearing();

        if (_audioplayer is not null && !_audioplayer.IsPlaying) PlayBackgroundMusic();
    }

    private async void PlayMenuSound(string path, int delay) {
        var audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(path));
        if (!audioPlayer.CanSeek) return;
        audioPlayer.Play();
        audioPlayer.Volume = Preferences.Default.Get("sfxvolume", 0.05);
        await Task.Delay(delay);
        audioPlayer.Stop();
        audioPlayer.Dispose();
    }

    private void HoverBeganLogoutButton(object sender, PointerEventArgs e) {
        PlayMenuSound("menubuttonhover.wav", 150);
        logoutLabel.TextColor = Colors.Red;
        var animation = new Animation {
            //{ 0, 1, new Animation(v => LogoutBorder.BackgroundColor = Color.FromRgba("ff00002b"), start: 0, end: 10000) },
            { 0, 1, new Animation(v => LogoutBorder.TranslationX = v, 0, -5) }
        };

        animation.Commit(LogoutBorder, "AnimateBackgroundColor", length: 250);
    }

    private void HoverEndedLogoutButton(object sender, PointerEventArgs e) {
        logoutLabel.TextColor = Colors.White;
        var animation = new Animation {
            //{ 0, 1, new Animation(v => LogoutBorder.BackgroundColor = Color.FromRgba("ff000000"), start: 3, end: 0) },
            { 0, 1, new Animation(v => LogoutBorder.TranslationX = v, -5, 0) }
        };

        animation.Commit(LogoutBorder, "AnimateBackgroundColor", length: 250);
    }

    private void LogoutTapped(object sender, TappedEventArgs e) {
        Navigation.PopAsync(false);
    }

    public void Receive(ItemBuyPriceMessage message) {
        coinsLabel.Text = message.Value.ToString();
    }
}