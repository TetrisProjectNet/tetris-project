using CommunityToolkit.Mvvm.Input;
using System.Net.Http.Json;
using System.Security.Claims;
using Tetris.Models;

namespace Tetris;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);
    }

    private void OnRegisterButtonClicked(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.google.com");
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

    private protected async Task LoginEvent()
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync($"https://localhost:7041/Auth/login/{usernameEntry.Text}/{passwordEntry.Text}", null);

            if (response.IsSuccessStatusCode)
            {
                SetLoginSuccessState();
                await Task.Delay(1200);
                var authToken = await response.Content.ReadAsStringAsync();

                var payload = authToken.Split('.')[1];
                var jsonBytes = ParseBase64WithoutPadding(payload);
                var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

                AuthorizedUser user = new AuthorizedUser(keyValuePairs["id"].ToString(), keyValuePairs[ClaimTypes.Name].ToString(), keyValuePairs[ClaimTypes.Role].ToString(), authToken, new DateTime(Convert.ToInt64(keyValuePairs["exp"].ToString())));
                await SecureStorage.Default.SetAsync("oauth_token", authToken);
                await Navigation.PushAsync(new MenuPage(), false);
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

    public void SetLoginSuccessState() {
        Label successLabel = (Label)FindByName("successLogin");
        successLabel.IsVisible = true;
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
}

