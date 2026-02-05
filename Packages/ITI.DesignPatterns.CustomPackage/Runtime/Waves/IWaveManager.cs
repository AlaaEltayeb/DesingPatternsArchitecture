namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public interface IWaveManager
    {
        bool WaveInProgress { get; }

        void StartNextWave();
        void UpdateWaveInProgress(bool waveInProgress);
    }
}