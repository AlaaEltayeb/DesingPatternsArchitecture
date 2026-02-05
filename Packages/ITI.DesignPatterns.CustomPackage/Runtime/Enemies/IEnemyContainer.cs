using System.Collections.Generic;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public interface IEnemyContainer
    {
        string EnemyPrefabId { get; }
        List<EnemyData> Enemies { get; }
    }
}