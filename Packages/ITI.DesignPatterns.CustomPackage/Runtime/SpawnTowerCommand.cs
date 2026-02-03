using ITI.DesignPatterns.Foundation.Runtime.Command;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public sealed class SpawnTowerCommand : ISyncCommand
    {
        private ITowerManager _towerManager;

        private readonly TowerType _towerId;

        [Inject]
        private void Inject(ITowerManager towerManager)
        {
            _towerManager = towerManager;
        }

        public SpawnTowerCommand(TowerType towerId)
        {
            _towerId = towerId;
        }

        public void Execute()
        {
            _towerManager.BuildTower(_towerId);
        }
    }
}