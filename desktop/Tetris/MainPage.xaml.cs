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
            Thread.Sleep(250);
            await Navigation.PushAsync(new MenuPage(), false);
        } else
        {
            Thread.Sleep(250);
            await DisplayAlert("Error", "Invalid username or password", "OK");
        }
    }

    private void PasswordEntry_OnCompleted(object sender, EventArgs e)
    {
        OnLoginButtonClicked(this, e);
    }
}

