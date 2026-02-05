using ITI.DesignPatterns.CustomPackage.Runtime.Enemy;
using ITI.DesignPatterns.CustomPackage.Runtime.Hud;
using ITI.DesignPatterns.Foundation.Runtime.Event;
using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class GameManager : MonoBehaviour, IGameManager
    {
        [Inject]
        private IEnemiesManager _enemiesManager;

        [Inject]
        private IWaveManager _waveManager;

        [Inject]
        private IEventSystem _eventSystem;

        [Inject]
        private GameDataModel _gameDataModel;

        [field: SerializeField]
        public Transform BulletParent { get; set; }

        public int Gold { get; private set; } = 200;

        public int Lives { get; private set; } = 20;

        public void UpdateGold(int newGold)
        {
            Gold += newGold;
        }

        public void UpdateLives(int newLives)
        {
            Lives += newLives;
        }

        private void Start()
        {
            _eventSystem.Subscribe<EnemyReachedBaseEvent>(OnEnemyReachedBase);
            _eventSystem.Subscribe<EnemyKilledEvent>(OnEnemyKilled);
        }

        private void OnEnemyKilled(EnemyKilledEvent evt)
        {
            UpdateGold(evt.Enemy.GoldRewards);
        }

        private void OnEnemyReachedBase(EnemyReachedBaseEvent evt)
        {
            UpdateLives(-evt.Enemy.DamageToBase);
        }

        private void OnDestroy()
        {
            _eventSystem.Unsubscribe<EnemyReachedBaseEvent>(OnEnemyReachedBase);
            _eventSystem.Unsubscribe<EnemyKilledEvent>(OnEnemyKilled);
        }

        private void Update()
        {
            if (Lives <= 0)
            {
                Lives = 0;
                if (_waveManager.WaveInProgress)
                {
                    _waveManager.UpdateWaveInProgress(false);
                    _gameDataModel.UpdateMessage("You Lose, (Reload Scene Manually)");
                }
            }

            if (_waveManager.WaveInProgress)
            {
                var anyAlive = false;
                for (var i = 0; i < _enemiesManager.Enemies.Count; i++)
                {
                    var e = _enemiesManager.Enemies[i];
                    if (e != null && _enemiesManager.Enemies[i].Hp > 0)
                    {
                        anyAlive = true;
                        break;
                    }
                }

                if (!anyAlive)
                {
                    _waveManager.UpdateWaveInProgress(false);
                    _gameDataModel.UpdateMessage("Wave Completed!");
                }
            }
        }
    }
}