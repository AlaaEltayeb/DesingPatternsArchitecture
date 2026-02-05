using System.Collections.Generic;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public interface IEnemyContainer
    {
        List<EnemyData> Enemies { get; }
    }
}