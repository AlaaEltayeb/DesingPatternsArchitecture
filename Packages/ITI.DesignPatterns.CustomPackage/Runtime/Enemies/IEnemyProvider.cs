namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public interface IEnemyProvider
    {
        bool TryGetEnemy(EnemyType enemyType, out EnemyData enemy);
    }
}