namespace ITI.DesignPatterns.CustomPackage.Runtime.Strategy
{
    public interface IAttackStrategyProvider
    {
        IAttackStrategy GetStrategy(TowerType towerType);
    }
}