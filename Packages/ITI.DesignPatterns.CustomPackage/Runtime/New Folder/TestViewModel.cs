using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using ITI.DesignPatterns.Foundation.Runtime.ViewBinding;

namespace ITI.DesignPatterns.CustomPackage.Runtime.New_Folder
{
    public sealed class TestViewModel : ViewModelBase
    {
        private readonly PlayerDataModel _playerDataModel;

        public BindableProperty<string> PlayerName => _playerDataModel.PlayerName;

        public TestViewModel(PlayerDataModel playerDataModel)
        {
            _playerDataModel = playerDataModel;
        }
    }
}