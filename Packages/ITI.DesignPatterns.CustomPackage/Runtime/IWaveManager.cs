namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public interface IWaveManager
    {
        int Wave { get; }
        bool WaveInProgress { get; }

        void StartNextWave();
        void UpdateWaveInProgress(bool waveInProgress);
    }
}