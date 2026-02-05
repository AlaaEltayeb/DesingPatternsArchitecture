using System;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies.EnemiesBehaviour
{
    public interface IEnemyStrategy : IDisposable
    {
        void Execute(Enemy enemy);
    }
}