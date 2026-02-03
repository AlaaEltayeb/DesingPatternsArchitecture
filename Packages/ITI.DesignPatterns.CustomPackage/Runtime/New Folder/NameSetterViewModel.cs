using ITI.DesignPatterns.Foundation.Runtime.MVVM;

namespace ITI.DesignPatterns.CustomPackage.Runtime.New_Folder
{
    public sealed class NameSetterViewModel : ViewModelBase
    {
        private readonly PlayerDataModel _playerDataModel;

        public NameSetterViewModel(PlayerDataModel playerDataModel)
        {
            _playerDataModel = playerDataModel;
        }

        public void UpdateName(string newName)
        {
            _playerDataModel.UpdatePlayerName(newName);
        }
    }
}