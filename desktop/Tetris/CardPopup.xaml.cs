using CommunityToolkit.Mvvm.ComponentModel;
using Mopups.Pages;
using SharpHook;
using SharpHook.Native;
using Tetris.ViewModels;

namespace Tetris;

public partial class CardPopupPage
{
    string name;
    ImageSource imageString;
    Color rectColor1;
    Color rectColor3;
    Color rectColor4;
    int coinCount;
    Thickness imageMargin = new Thickness(50, 150, 0, 0);
    CardView _card;
    TaskPoolGlobalHook hook;

    public CardPopupPage(string name, string imageString, Color rectColor1, Color rectColor3, Color rectColor4, int coinCount)
	{
		InitializeComponent();
		BindingContext = this;

        this.name = name;
        this.imageString = imageString;
        this.rectColor1 = rectColor1;
        this.rectColor3 = rectColor3;
        this.rectColor4 = rectColor4;

        if (imageString.ToString() == "fulllightblue.png") this.imageMargin = new Thickness(0, 150, 0, 0);
        if (imageString.ToString() == "fullyellow.png") this.imageMargin = new Thickness(20, 150, 0, 0);

        var a = imageString.ToString();

        
        _card = new CardView(new CardViewModel(0, 0, name, imageString, rectColor1, rectColor3, rectColor4, coinCount, true));
        _card.SetValue(Grid.RowProperty, 0);
        _card.SetValue(Grid.ColumnProperty, 0);
        _card.ScaleTo(1.8);
        Card.Children.Add(_card);

        hook = CreateKeypressListener();
        if (!hook.IsRunning) hook.RunAsync();
    }

    public TaskPoolGlobalHook CreateKeypressListener() {
        var hook = SingletonHook.Instance;
        hook.KeyPressed += OnKeyPressed;

        return hook;
    }

    public void OnKeyPressed(object sender, KeyboardHookEventArgs e) {

        if (e.Data.KeyCode == KeyCode.VcNumPadRight || e.Data.KeyCode == KeyCode.VcRight) {
            MainThread.BeginInvokeOnMainThread(() => {
                if (Window is null) return;
                _card.RightAnimation();
            });
        }

        if (e.Data.KeyCode == KeyCode.VcNumPadLeft || e.Data.KeyCode == KeyCode.VcLeft) {
            MainThread.BeginInvokeOnMainThread(() => {
                if (Window is null) return;
                _card.LeftAnimation();
            });
        }
    }

    //public void OnDisappear(object sender, EventArgs e) {
    //    if(!hook.IsDisposed) hook.Dispose();
    //}
}