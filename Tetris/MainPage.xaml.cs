using CommunityToolkit.Mvvm.Input;

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
        string username = usernameEntry.Text;
        string password = passwordEntry.Text;

        if (username == "test" && password == "test")
        {
            await Navigation.PushAsync(new MenuPage());
        } else
        {
            await DisplayAlert("Error", "Invalid username or password", "OK");
        }
    }

    private void OnForgotPasswordButtonClicked(object sender, EventArgs e)
    {
        var uri = new Uri("https://www.google.com");
        Launcher.OpenAsync(uri);
    }
}

