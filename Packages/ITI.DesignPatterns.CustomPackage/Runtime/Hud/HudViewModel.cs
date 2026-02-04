using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using ITI.DesignPatterns.Foundation.Runtime.ViewBinding;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Hud
{
    public sealed class HudViewModel : ViewModelBase
    {
        private readonly GameDataModel _gameDataModel;

        public BindableProperty<int> Gold => _gameDataModel.Gold;
        public BindableProperty<int> Lives => _gameDataModel.Lives;
        public BindableProperty<int> Wave => _gameDataModel.Wave;
        public BindableProperty<string> Message => _gameDataModel.Message;

        public HudViewModel(GameDataModel gameDataModel)
        {
            _gameDataModel = gameDataModel;
        }
    }
}