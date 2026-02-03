using ITI.DesignPatterns.Foundation.Runtime.Command;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public sealed class StartNewWaveCommand : ISyncCommand
    {
        private IWaveManager _waveManager;

        [Inject]
        private void Inject(IWaveManager waveManager)
        {
            _waveManager = waveManager;
        }

        public void Execute()
        {
            _waveManager.StartNextWave();
        }
    }
}