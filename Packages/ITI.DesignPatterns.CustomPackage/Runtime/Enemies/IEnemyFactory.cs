using System.Threading.Tasks;
using UnityEngine;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public interface IEnemyFactory
    {
        Task CreateEnemy(
            string name,
            EnemyType enemyType,
            Vector2 position,
            Transform parent = null);
    }
}