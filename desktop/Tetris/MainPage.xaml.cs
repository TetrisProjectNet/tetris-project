using CommunityToolkit.Mvvm.Input;
using Mopups.Interfaces;
using Mopups.Services;
using Plugin.Maui.Audio;
using SkiaSharp.Extended.UI.Controls;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Json;
using Tetris.Models;

namespace Tetris;

public partial class MainPage : ContentPage
{
    private static readonly string[] succesFields = new string[] {
        "Logged in, you will be redirected soon!",
        "This account is banned permanently.",
        "Your account has been terminated!"
    };

    public MainPage()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

        //if (SecureStorage.Default.GetAsync("oauth_token") is not null) AutomaticLogin();

        var token = SecureStorage.Default.GetAsync("oauth_token");
        if (token != null) {
            CheckIfUserHasBeenBanned(token.Result);
        }
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        var uri = new Uri("https://tetris-project-net-2.web.app/signup");
        Launcher.OpenAsync(uri);
    }

    private async void OnLoginButtonClicked(object sender, EventArgs e)
    {
        await LoginEvent();
    }

    private void PasswordEntry_OnCompleted(object sender, EventArgs e)
    {
        OnLoginButtonClicked(this, e);
    }

    private async void CheckIfUserHasBeenBanned(string token) {
        HttpClient httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

        var response = await httpClient.GetAsync("https://localhost:7041/Auth");

        if (response.IsSuccessStatusCode) {
            var authContent = await response.Content.ReadAsStringAsync();

            User user = JsonSerializer.Deserialize<User>(authContent);

            if (user.Banned) {
                SetLoginBannedState(2);
                return;
            } else {
                AutomaticLogin();
            }
        }
    }

    private protected async Task LoginEvent()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync($"https://localhost:7041/Auth/login/{usernameEntry.Text}/{passwordEntry.Text}", null);

            var content = await response.Content.ReadAsStringAsync();
            if (content.Contains("banned")) {
                SetLoginBannedState(1);
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                var authToken = await response.Content.ReadAsStringAsync();

                var payload = authToken.Split('.')[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                //AuthorizedUser authUser = new AuthorizedUser(keyValuePairs["id"].ToString(), keyValuePairs[ClaimTypes.Name].ToString(), keyValuePairs[ClaimTypes.Role].ToString(), authToken, new DateTime(Convert.ToInt64(keyValuePairs["exp"].ToString())));
                await SecureStorage.Default.SetAsync("oauth_token", authToken);

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", authToken);

                //var response2 = await httpClient.GetAsync("https://localhost:7041/Auth");

                //if (response2.IsSuccessStatusCode) {
                //    var authContent = await response2.Content.ReadAsStringAsync();

                //    User user = JsonSerializer.Deserialize<User>(authContent);

                //    if (user.Banned) {
                //        SetLoginBannedState();
                //        return;
                //    }
                //}
                SetLoginSuccessState();
                await Task.Delay(1200);
                await Navigation.PushAsync(new MenuPage(AudioManager.Current, MopupService.Instance), false);

                await Task.Delay(200);
                usernameEntry.Text = "";
                passwordEntry.Text = "";
            } else {
                Image usernameFail = (Image)FindByName("usernameFail");
                Image passwordFail = (Image)FindByName("passwordFail");
                usernameFail.IsVisible = true;
                passwordFail.IsVisible = true;
            }
        }
        catch (Exception e)
        {
            throw;
        }
    }

    public async void AutomaticLogin() {
        //await Task.Delay(1000);
        LoginButton.IsEnabled = false;
        usernameStack.IsVisible = false;
        passwordStack.IsVisible = false;
        AutomaticLoginAnimation.IsVisible = true;

        await Task.Delay(2000);
        AutomaticLoginAnimation.IsAnimationEnabled = !AutomaticLoginAnimation.IsAnimationEnabled;
        await Task.Delay(1900);
        AutomaticLoginAnimation.IsAnimationEnabled = !AutomaticLoginAnimation.IsAnimationEnabled;
        AutomaticLoginAnimation.IsVisible = false;
        await Task.Delay(250);

        await Navigation.PushAsync(new MenuPage(AudioManager.Current, MopupService.Instance), false);
    }

    public void SetLoginSuccessState() {
        successLogin.Text = succesFields[0];
        successLogin.TextColor = Colors.Green;
        successLogin.IsVisible = true;
    }

    public void SetLoginBannedState(int type) {
        successLogin.Text = succesFields[type];
        successLogin.TextColor = Colors.Red;
        successLogin.IsVisible = true;
    }

    public byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2:
                base64 += "==";
                break;
            case 3:
                base64 += "=";
                break;
        }
        return Convert.FromBase64String(base64);
    }

    protected override void OnAppearing() {
        base.OnAppearing();

        LoginButton.IsEnabled = true;
        usernameStack.IsVisible = true;
        passwordStack.IsVisible = true;
        AutomaticLoginAnimation.IsVisible = false;
        successLogin.IsVisible = false;
        usernameFail.IsVisible = false;
        passwordFail.IsVisible = false;
    }
}

