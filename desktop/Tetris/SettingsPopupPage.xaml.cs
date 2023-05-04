using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Graphics.Text;
using Mopups.Services;
using Plugin.Maui.Audio;
using SkiaSharp.Extended.UI.Controls;
using System.Data.Common;
using Tetris.Messages;
using Tetris.Models;
using Tetris.References;
using Tetris.ViewModels;

namespace Tetris;

public partial class SettingsPopupPage {
    private User _user;
    private IAudioPlayer _audioPlayer;
    List<string> _skinNames;

    private static readonly Color[] _fadeColors = new Color[] {
        Color.FromRgba("00ffff"),
        Color.FromRgba("00ff00"),
        Color.FromRgba("ffff00"),
        Color.FromRgba("ff7f00"),
        Color.FromRgba("ff0000"),
    };

    public SettingsPopupPage(User user, IAudioPlayer audioPlayer) {
        InitializeComponent();
        _user = user;
        _audioPlayer = audioPlayer;
        MusicSlider.Value = Preferences.Default.Get("musicvolume", 0.1);
        SFXSlider.Value = Preferences.Default.Get("sfxvolume", 0.05);

        initCards();
    }

    private void BackButtonClicked(object sender, EventArgs e) {
        MopupService.Instance.PopAllAsync();
    }

    private void initCards() {
        SettingsCardView cardview;
        _skinNames = new List<string>();

        if (_user.ShopItems is not null) {
            for (int i = 0; i < _user.ShopItems.Length; i++) {
                _skinNames.Add(_user.ShopItems[i].Title);
            }
        }
        _skinNames.Insert(0, "Reset");

        for (int i = 0; i < 7; i++) {
            cardview = new SettingsCardView(new SettingsCardViewModel(ShopItemIdReferences.defaultSettingItems[i].Name, ShopItemIdReferences.defaultSettingItems[i].Image, ShopItemIdReferences.defaultSettingItems[i].ArrowImage, ShopItemIdReferences.defaultSettingItems[i].Color, _skinNames, i));
            CardStack.Children.Add(cardview);
        }
    }

    private void MusicSlider_ValueChanged(object sender, ValueChangedEventArgs e) {
        double value = e.NewValue;
        Preferences.Default.Set("musicvolume", value);
        _audioPlayer.Volume = value;

        MusicValue.Text = CalculatePercentage(value) + "%";
        MusicValue.TextColor = InterpolateColor(value);
    }

    private void SFXSlider_ValueChanged(object sender, ValueChangedEventArgs e) {
        double value = e.NewValue;
        Preferences.Default.Set("sfxvolume", value);

        SFXValue.Text = CalculatePercentage(value) + "%";
        SFXValue.TextColor = InterpolateColor(value);
    }

    private Color InterpolateColor(double value) {
        int colorCount = _fadeColors.Length;
        double colorIndex = value * (colorCount - 1) * 5;
        int index1 = (int)Math.Floor(colorIndex);
        int index2 = (int)Math.Ceiling(colorIndex);
        if (index2 == 5) index2 = colorCount - 1;
        double fraction = colorIndex - index1;

        Color minColor = _fadeColors[index1];
        Color maxColor = _fadeColors[index2];

        byte r = (byte)(minColor.Red * byte.MaxValue + (maxColor.Red * byte.MaxValue - minColor.Red * byte.MaxValue) * (fraction));
        byte g = (byte)(minColor.Green * byte.MaxValue + (maxColor.Green * byte.MaxValue - minColor.Green * byte.MaxValue) * (fraction));
        byte b = (byte)(minColor.Blue * byte.MaxValue + (maxColor.Blue * byte.MaxValue - minColor.Blue * byte.MaxValue) * (fraction));

        return Color.FromRgb(r, g, b);
    }

    private int CalculatePercentage(double value) {
        return Convert.ToInt32(value * 500);
    }

    private void MenuBackgroundTapped(object sender, TappedEventArgs e) {
        WeakReferenceMessenger.Default.Send(new CloseSelectMenuMessage(-1));
    }

    private void BackButtonHoverBegan(object sender, PointerEventArgs e) {
        (sender as Button).TextColor = Color.FromArgb("4ED078");
    }

    private void BackButtonHoverEnd(object sender, PointerEventArgs e) {
        (sender as Button).TextColor = Colors.White;
    }
}