using ITI.DesignPatterns.Foundation.Runtime.ViewBinding;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Hud
{
    public sealed class GameDataModel
    {
        public BindableProperty<int> Gold { get; private set; } = new(200);
        public BindableProperty<int> Lives { get; private set; } = new(20);
        public BindableProperty<int> Wave { get; private set; } = new(1);
        public BindableProperty<string> Message { get; private set; } = new();

        public void UpdateGold(int newGold)
        {
            Gold.Value += newGold;
        }

        public void UpdateLives(int newLives)
        {
            Lives.Value += newLives;
        }

        public void UpdateWave()
        {
            Wave.Value++;
        }

        public void UpdateMessage(string newMessage)
        {
            Message.Value = newMessage;
        }
    }
}