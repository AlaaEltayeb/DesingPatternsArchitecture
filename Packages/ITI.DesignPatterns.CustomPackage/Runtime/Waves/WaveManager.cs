using ITI.DesignPatterns.CustomPackage.Runtime.Enemies;
using ITI.DesignPatterns.CustomPackage.Runtime.Hud;
using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Waves
{
    public class WaveManager : MonoBehaviour, IWaveManager
    {
        [Inject]
        private IEnemiesManager _enemiesManager;

        [Inject]
        private GameDataModel _gameDataModel;

        public bool WaveInProgress { get; private set; }

        public void UpdateWaveInProgress(bool waveInProgress)
        {
            WaveInProgress = waveInProgress;
        }

        public void StartNextWave()
        {
            if (WaveInProgress)
                return;

            _gameDataModel.UpdateWave();
            WaveInProgress = true;

            CancelInvoke(nameof(_enemiesManager.SpawnFromSequence));
            _enemiesManager.ResetWave();
            InvokeRepeating(nameof(_enemiesManager.SpawnFromSequence), 0.25f, 0.6f);
        }
    }
}