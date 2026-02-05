using ITI.DesignPatterns.CustomPackage.Runtime.Enemies;
using ITI.DesignPatterns.CustomPackage.Runtime.Hud;
using ITI.DesignPatterns.Foundation.Runtime.Event;
using System;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime.GamePlay
{
    public class GameManager : IGameManager, IDisposable
    {
        [Inject]
        private IEventSystem _eventSystem;

        [Inject]
        private GameDataModel _gameDataModel;

        public void Init()
        {
            _eventSystem.Subscribe<EnemyReachedBaseEvent>(OnEnemyReachedBase);
            _eventSystem.Subscribe<EnemyKilledEvent>(OnEnemyKilled);
        }

        private void OnEnemyKilled(EnemyKilledEvent evt)
        {
            _gameDataModel.UpdateGold(evt.GoldReward);
        }

        private void OnEnemyReachedBase(EnemyReachedBaseEvent evt)
        {
            _gameDataModel.UpdateLives(-evt.EnemyDamageToBase);
        }

        private void Update()
        {
            //if (Lives <= 0)
            //{
            //    Lives = 0;
            //    if (_waveManager.WaveInProgress)
            //    {
            //        _waveManager.UpdateWaveInProgress(false);
            //        _gameDataModel.UpdateMessage("You Lose, (Reload Scene Manually)");
            //    }
            //}

            //if (_waveManager.WaveInProgress)
            //{
            //    var anyAlive = false;
            //    for (var i = 0; i < _enemiesManager.Enemies.Count; i++)
            //    {
            //        var e = _enemiesManager.Enemies[i];
            //        if (e != null && _enemiesManager.Enemies[i].Hp > 0)
            //        {
            //            anyAlive = true;
            //            break;
            //        }
            //    }

            //    if (!anyAlive)
            //    {
            //        _waveManager.UpdateWaveInProgress(false);
            //        _gameDataModel.UpdateMessage("Wave Completed!");
            //    }
            //}
        }

        public void Dispose()
        {
            _eventSystem.Unsubscribe<EnemyReachedBaseEvent>(OnEnemyReachedBase);
            _eventSystem.Unsubscribe<EnemyKilledEvent>(OnEnemyKilled);
        }
    }
}