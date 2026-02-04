using System.Collections.Generic;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Strategy
{
    public sealed class AttackStrategyProvider : IAttackStrategyProvider
    {
        private readonly Dictionary<TowerType, IAttackStrategy> _strategies = new()
        {
            { TowerType.Gunner, new GunAttackStrategy() },
            { TowerType.Cannon, new CannonAttackStrategy() },
            { TowerType.Frost, new FrostAttackStrategy() },
        };

        public IAttackStrategy GetStrategy(TowerType towerType) => _strategies[towerType];
    }
}