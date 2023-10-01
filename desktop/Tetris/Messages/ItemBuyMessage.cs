using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Tetris.Messages;
public class ItemBuyMessage : ValueChangedMessage<string> {
    public ItemBuyMessage(string value) : base(value) {
    }
}