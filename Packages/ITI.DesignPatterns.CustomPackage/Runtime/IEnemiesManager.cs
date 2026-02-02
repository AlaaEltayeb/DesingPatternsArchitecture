using System.Collections.Generic;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public interface IEnemiesManager
    {
        string[] Waves { get; }
        List<UglyEnemy> Enemies { get; }

        void ResetWave(int wave);
        void SpawnFromSequence();

        void EnemyReachedBase(UglyEnemy enemy);
        void EnemyKilled(UglyEnemy enemy);
    }
}