using CommunityToolkit.Mvvm.Messaging;
using Tetris.Messages;

namespace Tetris.ViewModels;

public partial class SettingsCardView : ContentView, IRecipient<CloseSelectMenuMessage>
{
    public SettingsCardViewModel scvm;
    private bool _animationIsPlaying;
    private int buttonState = 0;
    public SettingsCardView(SettingsCardViewModel scvm)
	{
		InitializeComponent();
        BindingContext = scvm;
        this.scvm = scvm;

        WeakReferenceMessenger.Default.Register<CloseSelectMenuMessage>(this);

        if (scvm.SkinNames is not null) PopulateSelectMenu(scvm.SkinNames.Count);

        string saved = Preferences.Default.Get($"skinsUsed", "0000000");
        if (Convert.ToByte(saved[scvm._colNumber]) == '1') {
            PlayAnimation();
        }
    }

    private void LottieTapped(object sender, TappedEventArgs e) {
        ChangeSkinUsedState();
        PlayAnimation();
    }

    private void ChangeSkinUsedState() {
        string text = Preferences.Default.Get($"skinsUsed", "0000000");
        string newValue = string.Empty;

        for (int i = 0; i < text.Length; i++) {
            if (i == scvm._colNumber) {
                newValue += text[i] == '1' ? '0' : '1';
                continue;
            }
            newValue += text[i];
        }
        Preferences.Default.Set($"skinsUsed", newValue);
    }

    private async void PlayAnimation() {
        if (_animationIsPlaying) return;

        buttonState = buttonState == 1 ? 0 : 1;
        _animationIsPlaying = true;
        switchButton.IsAnimationEnabled = !switchButton.IsAnimationEnabled;

        await Task.Delay(1533);
        switchButton.IsAnimationEnabled = !switchButton.IsAnimationEnabled;

        var ts = buttonState == 1 ? TimeSpan.FromMilliseconds(1533) : TimeSpan.FromMilliseconds(0);
        switchButton.Progress = ts;
        _animationIsPlaying = false;
    }

    private void SelectTap(object sender, EventArgs e) {
        if (SelectMenu.IsVisible) {
            CloseSelectMenu();
            return;
        }
        ExtractSelectMenu();
        WeakReferenceMessenger.Default.Send(new CloseSelectMenuMessage(scvm._colNumber));
    }

    private void ExtractSelectMenu() {
        SelectMenu.IsVisible = true;
        int height = 120;
        if (SelectStack.Children.Count == 1) height = 40;
        var animation = new Animation {
            { 0, 1, new Animation(v => SelectMenu.HeightRequest = v, 0, height) }
        };

        animation.Commit(SelectMenu, "ExtractSelectMenu", length: 150);
    }

    private void CloseSelectMenu() {
        int height = 120;
        if (SelectStack.Children.Count == 1) height = 40;
        var animation = new Animation {
            { 0, 1, new Animation(v => SelectMenu.HeightRequest = v, height, 0) }
        };

        animation.Commit(SelectMenu, "CloseSelectMenu", length: 100, finished: (d, b) => {
            SelectMenu.IsVisible = false;
        });
    }

    private void PopulateSelectMenu(int count) {
        for (int i = 0; i < count; i++) {
            SelectStack.Children.Add(new SettingsDropDownItem(scvm.SkinNames[i], this));
        }
    }

    internal void MenuItemSelectEvent(string name) {
        scvm.HandleSelectedSkin(name);
        CloseSelectMenu();
    }

    public void Receive(CloseSelectMenuMessage message) {
        MainThread.BeginInvokeOnMainThread(() => {
            HandleCloseMessage(message);
        });
    }

    private void HandleCloseMessage(CloseSelectMenuMessage m) {
        if (m.Value != scvm._colNumber && SelectMenu.IsVisible) CloseSelectMenu();
    }

    private void SettingsCardHoverBegan(object sender, PointerEventArgs e) {
        (sender as Border).ScaleTo(1.06);
    }

    private void SettingsCardHoverEnd(object sender, PointerEventArgs e) {
        (sender as Border).ScaleTo(1);
    }

    private void SelectMenuHoverBegan(object sender, PointerEventArgs e) {
        SelectLabel.TextColor = scvm.RectColor;
    }

    private void SelectMenuHoverEnd(object sender, PointerEventArgs e) {
        SelectLabel.TextColor = Colors.White;
    }
}