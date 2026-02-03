using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ITI.DesignPatterns.CustomPackage.Runtime.New_Folder
{
    public sealed class TestView : ViewBase<TestViewModel>
    {
        [SerializeField]
        private Button _button;

        [SerializeField]
        private TextMeshProUGUI _playerNameText;

        [SerializeField]
        private Image _playerImage;

        protected override void Bind()
        {
            //bind button to ViewModel.command
            //bind text to ViewModel.playername
            //bind image to ViewModel.playerimage

            ViewModel.PlayerName.StartObserving(OnPlayerNameChanged);
        }

        private void OnPlayerNameChanged(string newValue)
        {
            _playerNameText.text = newValue;
        }
    }
}