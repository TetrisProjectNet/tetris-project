using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Maui.Controls;
using Mopups.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Tetris.Messages;
using Tetris.Models;
using Tetris.References;
using Tetris.Services;
using Tetris.ViewModels;

namespace Tetris;

public partial class ShopPopupPage : IRecipient<ItemBuyMessage>
{
    private User _user;
    private int? sortedWay;
    private ShopPopupViewModel _spvm;
    private enum SortWays {
        forward,
        backwards
    }
	public ShopPopupPage(User user, ShopPopupViewModel spvm)
	{
		InitializeComponent();
        _user = user;
        _spvm = spvm;

        WeakReferenceMessenger.Default.Register<ItemBuyMessage>(this);
        Task.Run(FetchCards);
	}

	private void BackButtonClicked(object sender, EventArgs e) {
		MopupService.Instance.PopAllAsync();
	}

    private void HoverBegan(object sender, PointerEventArgs e) {
        ((Border)sender).ScaleTo(1.1);
    }

    private void HoverEnded(object sender, PointerEventArgs e) {
        ((Border)sender).ScaleTo(1.0);
    }

    private void ClearCardGrid() {
        int count = CardGrid.Children.Count;
        for (int i = 0; i < count; i++) {
            CardGrid.RemoveAt(0);
        }
    }

    public void FillCardGrid() {
        //var cardView = new CardView(new CardViewModel(2, 2, "Alma", "fullred.png", Color.FromArgb("#2F0766"), Color.FromArgb("#2F0766"), Color.FromArgb("#2F0766"), 20));

        //cardView.SetValue(Grid.RowProperty, 2);
        //cardView.SetValue(Grid.ColumnProperty, 2);
        //CardGrid.Children.Add(cardView);

        if (CardGrid.Children.Count != 0) ClearCardGrid();

        CardView cardView;
        int row = 0;
        int column = 1;
        int shopitemPlaced = 0;

        List<ShopItem> bought = new List<ShopItem>();
        Color color = Color.FromArgb("#2F0766");

        for (int i = 0; i < _spvm.ShopItems.Length; i++) {
            bool alreadyBought = CheckForAlreadyBought(_spvm.ShopItems[i].Id);
            if (alreadyBought) {
                bought.Add(_spvm.ShopItems[i]);
                continue;
            }
            cardView = new CardView(new CardViewModel(0, 0, _spvm.ShopItems[i].Title, _spvm.ShopItems[i].Image, color, color, color, _spvm.ShopItems[i].Price, false));
            if (shopitemPlaced % 5 == 0) {
                row++;
                column = 1;
            }
            cardView.SetValue(Grid.RowProperty, row);
            cardView.SetValue(Grid.ColumnProperty, column);
            column++;
            shopitemPlaced++;
            CardGrid.Children.Add(cardView);
        }

        color = Color.FromArgb("#AA3434");
        for (int i = 0; i < bought.Count; i++) {
            if (shopitemPlaced % 5 == 0) {
                row++;
                column = 1;
            }
            cardView = new CardView(new CardViewModel(row, column, bought[i].Title, bought[i].Image, color, color, color, bought[i].Price, false));
            cardView.SetValue(Grid.RowProperty, row);
            cardView.SetValue(Grid.ColumnProperty, column);
            column++;
            shopitemPlaced++;
            CardGrid.Children.Add(cardView);
        }

        //ShopPictureService pictureService = new ShopPictureService();

        //for (int i = 0; i < items.Count; i++) {
        //    pictureService.GenerateImage(items[i].Image);
        //}
    }

    private void SortButtonClicked(object sender, EventArgs e) {
        if (sortedWay is null) sortedWay = (int?)SortWays.backwards;
        sortedWay = sortedWay == (int)SortWays.backwards ? (int?)SortWays.forward : (int?)SortWays.backwards;
        for (int i = 0; i < CardGrid.Children.Count - 1; i++) {
            for (int j = 0; j < CardGrid.Children.Count - i - 1; j++) {
                var card1 = CardGrid.Children[j] as CardView;
                var card2 = CardGrid.Children[j + 1] as CardView;
                int price1 = ((CardView)CardGrid.Children[j]).cvm.CoinCount;
                int price2 = ((CardView)CardGrid.Children[j + 1]).cvm.CoinCount;
                if (sortedWay == (int?)SortWays.forward) {
                    if (price1 > price2) SwapCards(card1, card2, i, j);
                }
                if (sortedWay == (int?)SortWays.backwards) {
                    if (price1 < price2) SwapCards(card1, card2, i, j);
                }
            }
        }
    }

    private void SwapCards(CardView card1, CardView card2, int i, int j) {
        int row1 = (int)card1.GetValue(Grid.RowProperty);
        int row2 = (int)card2.GetValue(Grid.RowProperty);
        int col1 = (int)card1.GetValue(Grid.ColumnProperty);
        int col2 = (int)card2.GetValue(Grid.ColumnProperty);
        ((CardView)CardGrid.Children[j]).SetValue(Grid.RowProperty, row2);
        ((CardView)CardGrid.Children[j]).SetValue(Grid.ColumnProperty, col2);
        ((CardView)CardGrid.Children[j + 1]).SetValue(Grid.RowProperty, row1);
        ((CardView)CardGrid.Children[j + 1]).SetValue(Grid.ColumnProperty, col1);
        CardGrid.Children.RemoveAt(j);
        CardGrid.Children.Insert(j + 1, card1);
    }

    private bool CheckForAlreadyBought(string id) {
        if (_user.ShopItems is null) return false;
        for (int i = 0; i < _user.ShopItems.Length; i++) {
            if (_user.ShopItems[i].Id == id) return true;
        }
        return false;
    }

    private void SearchEntry_Completed(object sender, EventArgs e) {
        HandleSaarchEvent();
    }

    private void HandleSaarchEvent() {
        FillCardGrid();

        var searchText = SearchEntry.Text;
        if (searchText.Length == 0) return;
        searchText.ToLower();

        int row = 1;
        int column = 1;
        string cardText;
        CardView card;

        for (int i = 0; i < CardGrid.Children.Count;) {
            card = (CardGrid.Children[i] as CardView);
            cardText = card.cvm.Name;
            if (cardText.ToLower().Contains(searchText)) {
                card.IsVisible = true;
                card.SetValue(Grid.RowProperty, row);
                card.SetValue(Grid.ColumnProperty, column);
                if (column == 5) {
                    row++;
                    column = 1;
                } else {
                    column++;
                }
                i++;
            } else {
                CardGrid.Children.RemoveAt(i);
            }
        }
    }

    public async void FetchCards() {
        //List<ShopItem> shopItems = new List<ShopItem>() {
        //    new ShopItem("63e7bf4b0123bcd37677b4cf", 0, "Shiny Ruby" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinyrubyZ.png"), 2500, "Black", "Test"),
        //    new ShopItem("63e7bf550b63bcd37435b4d0", 1, "Shiny Orange" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinyorangeZ.png"), 2500, "Black", "Test"),
        //    new ShopItem("6429526f37eedddf34568a9d", 2, "Shiny Light Blue" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinylightblueZ.png"), 2500, "Black", "Test"),
        //    new ShopItem("644833fcaa58ac24c2b66a49", 3, "Shiny Green" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinygreenZ.png"), 2500, "Black", "Test"),
        //    new ShopItem("63e7bf550121b63bc017b4d0", 4, "Shiny Dark Blue" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinydarkblueZ.png"), 2500, "Black", "Test"),
        //    new ShopItem("63e7bf4b0121b63bcd3454cf", 5, "Shiny Violet" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinyvioletZ.png"), 2500, "Black", "Test"),
        //    new ShopItem("6429526f37008eedddf31a9d", 6, "Shiny Yellow" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullshinyyellowZ.png"), 2500, "Black", "Test"),
        //    new ShopItem("644833fcaa58ac57c2b66a49", 7, "Boy Crown" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullyellowboycrownZ.png"), 3000, "Black", "Test"),
        //    new ShopItem("63e7bf550121b63bcd37b4d0", 8, "Girl Crown" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullyellowgirlcrownZ.png"), 3000, "Black", "Test"),
        //    new ShopItem("63e7bf4b0121b63bcd37b4cf", 9, "Cloudy" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullcloudyZ.png"), 2000, "Black", "Test"),
        //    new ShopItem("6429526f37008eedddf68a9d", 10, "Peach" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullpeachZ.png"), 1200, "Black", "Test"),
        //    new ShopItem("6429526f37008eedddf34a9d", 11, "Yellow X" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullyellowxZ.png"), 3500, "Black", "Test"),
        //    new ShopItem("6429526f37428eedddf68a9d", 12, "Minecraft" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullminecraftZ.png"), 1500, "Black", "Test"),
        //    new ShopItem("6429526f37008ee53df68a9d", 13, "Foxtile" , Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\ShopImages\\fullfoxZ.png"), 4000, "Black", "Test"),
        //};
        if (_spvm.ShopItems is null) {
            HttpClient httpClient = new HttpClient();


            var response = await httpClient.GetAsync("https://localhost:7041/ShopItem");

            if (response.IsSuccessStatusCode) {
                var shopContent = await response.Content.ReadAsStringAsync();

                _spvm.ShopItems = JsonSerializer.Deserialize<ShopItem[]>(shopContent);
                _spvm.OriginShopItems = JsonSerializer.Deserialize<ShopItem[]>(shopContent);
            }

            for (int i = 0; i < _spvm.ShopItems.Length; i++) {
                _spvm.ShopItems[i].Image = ShopItemIdReferences._ShopImageReferences[i].Image.Replace(".png", "Z.png");
            }
            await Task.Delay(3000);
        } else {
            await Task.Delay(500);
        }
        MainThread.BeginInvokeOnMainThread(() => {
            Task.Run(RemoveLoadingAnimation);
            FillCardGrid();
        });
    }

    private async Task RemoveLoadingAnimation() {
        await Task.Delay(400);
        MainThread.BeginInvokeOnMainThread(() => CardGridStack.Remove(LoadingAnimation));
    }

    private void ButtonHoverBegan(object sender, PointerEventArgs e) {
        (sender as Button).TextColor = Color.FromArgb("4ED078");
    }

    private void ButtonHoverEnd(object sender, PointerEventArgs e) {
        (sender as Button).TextColor = Colors.White;
    }

    void IRecipient<ItemBuyMessage>.Receive(ItemBuyMessage message) {
        MainThread.BeginInvokeOnMainThread(() => {
            ShopItem item = _spvm.OriginShopItems.Where(x => x.Title == message.Value).First();
            if (_user.Coins >= item.Price) {
                _user.Coins -= item.Price;
                //var arr = _user.ShopItems;
                //Array.Resize(ref arr, _user.ShopItems.Length + 1);
                //arr[_user.ShopItems.Length - 1] = item;
                //_user.ShopItems = arr;
                if (_user.ShopItems is not null) {
                    _user.ShopItems = _user.ShopItems.Append(item).ToArray();
                } else {
                    _user.ShopItems = new ShopItem[] { item };
                }
                SendDataToServer(_user.Coins, item);
                FillCardGrid();
                WeakReferenceMessenger.Default.Send(new ItemBuyPriceMessage(_user.Coins));

                MopupService.Instance.PushAsync(new PurchasePage(true));
            } else {
                MopupService.Instance.PushAsync(new PurchasePage(false));
            }

        });
    }

    public async void SendDataToServer(int newCoin, ShopItem newItem) {
        HttpClient httpClient = new HttpClient();
        string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", oauthToken);

        var response = await httpClient.GetAsync("https://localhost:7041/Auth");

        if (response.IsSuccessStatusCode) {
            var authContent = await response.Content.ReadAsStringAsync();

            UserDTO user = JsonSerializer.Deserialize<UserDTO>(authContent);

            if (user.ShopItems is not null) {
                ShopItem[] newArray = new ShopItem[user.ShopItems.Length + 1];
                Array.Copy(user.ShopItems, newArray, user.ShopItems.Length);
                newArray[newArray.Length - 1] = newItem;
                user.ShopItems = newArray;
            } else {
                user.ShopItems = new ShopItem[] { newItem };
            }
            user.Coins = newCoin;

            var json = JsonSerializer.Serialize(user);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"https://localhost:7041/User/{user.Id}") {
                Content = new StringContent(json, Encoding.UTF8, "application/json-patch+json")
            };

            response = await httpClient.SendAsync(request);
        }
    }
}