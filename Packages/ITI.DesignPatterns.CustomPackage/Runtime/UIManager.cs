using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class UIManager : MonoBehaviour, IUIManager
    {
        [Inject]
        private IGameManager _gameManager;

        [Inject]
        private IWaveManager _waveManager;

        [Inject]
        private ITowerManager _towerManager;

        public TextMeshProUGUI GoldText;
        public TextMeshProUGUI WaveText;
        public TextMeshProUGUI LivesText;

        private TextMeshProUGUI _messageText;
        public Button StartWaveButton;

        public Button BuildGunnerButton;
        public Button BuildCannonButton;
        public Button BuildFrostButton;

        private void Start()
        {
            RefreshUI();

            if (StartWaveButton != null)
                StartWaveButton.onClick.AddListener(() => _waveManager.StartNextWave());

            if (BuildGunnerButton != null)
                BuildGunnerButton.onClick.AddListener(() => _towerManager.BuildTower(TowerType.Gunner));
            if (BuildCannonButton != null)
                BuildCannonButton.onClick.AddListener(() => _towerManager.BuildTower(TowerType.Cannon));
            if (BuildFrostButton != null)
                BuildFrostButton.onClick.AddListener(() => _towerManager.BuildTower(TowerType.Frost));
        }

        public void UpdateInGameMessage(string newMessage)
        {
            _messageText.text = newMessage;
        }

        public void RefreshUI()
        {
            if (GoldText != null)
                GoldText.text = $"Gold: {_gameManager.Gold}";
            if (WaveText != null)
                WaveText.text = "Wave: " + _waveManager.Wave
                    + (_waveManager.WaveInProgress ? " (Running)" : "");
            if (LivesText != null)
                LivesText.text = $"Lives: {_gameManager.Lives}";
        }
    }
}