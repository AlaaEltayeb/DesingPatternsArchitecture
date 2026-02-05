using System.Collections.Generic;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public interface IEnemiesManager
    {
        string[] Waves { get; }
        List<Enemy> Enemies { get; }

        void ResetWave();
        void SpawnFromSequence();
    }
}