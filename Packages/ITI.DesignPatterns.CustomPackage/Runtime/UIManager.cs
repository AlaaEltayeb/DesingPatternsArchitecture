using ITI.DesignPatterns.Foundation.Runtime.Command;
using ITI.DesignPatterns.Foundation.Runtime.Event;
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
        private IEventSystem _eventSystem;

        [Inject]
        private ICommandDispatcher _commandDispatcher;

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
                StartWaveButton.onClick.AddListener(StartNextWave);

            if (BuildGunnerButton != null)
                BuildGunnerButton.onClick.AddListener(() => BuildTower(TowerType.Gunner));
            if (BuildCannonButton != null)
                BuildCannonButton.onClick.AddListener(() => BuildTower(TowerType.Cannon));
            if (BuildFrostButton != null)
                BuildFrostButton.onClick.AddListener(() => BuildTower(TowerType.Frost));

            _eventSystem.Subscribe<EnemyReachedBaseEvent>(UpdateUI);
            _eventSystem.Subscribe<EnemyKilledEvent>(UpdateUI);
        }

        private void BuildTower(TowerType towerType)
        {
            _commandDispatcher.RegisterAndExecute(() => new SpawnTowerCommand(towerType));
        }

        private void StartNextWave()
        {
            _commandDispatcher.RegisterAndExecute(() => new StartNewWaveCommand());
        }

        private void UpdateUI<TEvent>(TEvent evt) where TEvent : IEvent
        {
            RefreshUI();
        }

        private void OnDestroy()
        {
            _eventSystem.Unsubscribe<EnemyReachedBaseEvent>(UpdateUI);
            _eventSystem.Unsubscribe<EnemyKilledEvent>(UpdateUI);
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