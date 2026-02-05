using System.Collections.Generic;
using System.Linq;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Turrets
{
    public sealed class TurretProvider : ITurretProvider
    {
        private readonly List<TurretData> _turretData;

        public TurretProvider(ITurretContainer turretContainer)
        {
            _turretData = turretContainer.Turrets;
        }

        public TurretData GetTurretData(TowerType towerType) =>
            _turretData.FirstOrDefault(turret => turret.TowerId == towerType);
    }
}