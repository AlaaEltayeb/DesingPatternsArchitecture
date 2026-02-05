using ITI.DesignPatterns.Foundation.Runtime.Command;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Turrets
{
    public sealed class SpawnTowerCommand : ISyncCommand
    {
        private ITurretFactory _turretFactory;

        private readonly TowerType _towerId;

        [Inject]
        private void Inject(ITurretFactory turretFactory)
        {
            _turretFactory = turretFactory;
        }

        public SpawnTowerCommand(TowerType towerId)
        {
            _towerId = towerId;
        }

        public void Execute()
        {
            _turretFactory.BuildTower(_towerId);
        }
    }
}