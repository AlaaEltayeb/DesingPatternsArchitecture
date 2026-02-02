using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class WaveManager : MonoBehaviour, IWaveManager
    {
        [Inject]
        private IEnemiesManager _enemiesManager;

        [Inject]
        private IUIManager _uiManager;

        public int Wave { get; private set; }
        public bool WaveInProgress { get; private set; }

        public void UpdateWaveInProgress(bool waveInProgress)
        {
            WaveInProgress = waveInProgress;
        }

        public void StartNextWave()
        {
            if (WaveInProgress)
                return;

            Wave++;
            WaveInProgress = true;
            _uiManager.RefreshUI();

            CancelInvoke(nameof(_enemiesManager.SpawnFromSequence));
            _enemiesManager.ResetWave(Wave);
            InvokeRepeating(nameof(_enemiesManager.SpawnFromSequence), 0.25f, 0.6f);
        }
    }
}