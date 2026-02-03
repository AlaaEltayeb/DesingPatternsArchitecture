using ITI.DesignPatterns.Foundation.Runtime.ViewBinding;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.New_Folder
{
    public sealed class PlayerDataModel
    {
        public BindableProperty<string> PlayerName { get; } = new("Guest");
        public BindableProperty<Sprite> PlayerImage { get; }
        public BindableProperty<int> Score { get; } = new(100);

        public void UpdatePlayerName(string newName)
        {
            PlayerName.Value = newName;
        }
    }
}