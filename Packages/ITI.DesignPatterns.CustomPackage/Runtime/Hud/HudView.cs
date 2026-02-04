using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using TMPro;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Hud
{
    public sealed class HudView : ViewBase<HudViewModel>
    {
        [SerializeField]
        private TextMeshProUGUI _goldText;

        [SerializeField]
        private TextMeshProUGUI _livesText;

        [SerializeField]
        private TextMeshProUGUI _waveText;

        [SerializeField]
        private TextMeshProUGUI _messageText;

        protected override void Bind()
        {
            ViewModel.Gold.StartObserving(OnGoldChanged);
            ViewModel.Lives.StartObserving(OnLivesChanged);
            ViewModel.Wave.StartObserving(OnWaveChanged);
            ViewModel.Message.StartObserving(OnMessageChanged);
        }

        private void OnMessageChanged(string newvalue)
        {
            _messageText.text = newvalue;
        }

        private void OnWaveChanged(int newvalue)
        {
            _waveText.text = newvalue.ToString();
        }

        private void OnLivesChanged(int newvalue)
        {
            _livesText.text = newvalue.ToString();
        }

        private void OnGoldChanged(int newvalue)
        {
            _goldText.text = newvalue.ToString();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            ViewModel.Gold.StopObserving(OnGoldChanged);
            ViewModel.Lives.StopObserving(OnLivesChanged);
            ViewModel.Wave.StopObserving(OnWaveChanged);
            ViewModel.Message.StopObserving(OnMessageChanged);
        }
    }
}