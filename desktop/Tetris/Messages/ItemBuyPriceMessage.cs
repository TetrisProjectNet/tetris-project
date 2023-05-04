using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Tetris.Messages; 
public class ItemBuyPriceMessage : ValueChangedMessage<int> {
    public ItemBuyPriceMessage(int value) : base(value) {
    }
}
