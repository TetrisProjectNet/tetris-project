using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Tetris.Messages;
public class CloseSelectMenuMessage : ValueChangedMessage<int> {
    public CloseSelectMenuMessage(int value) : base(value) {
    }
}
