using ITI.DesignPatterns.Foundation.Runtime.Event;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public struct EnemyReachedBaseEvent : IEvent
    {
        public UglyEnemy Enemy { get; }

        public EnemyReachedBaseEvent(UglyEnemy enemy)
        {
            Enemy = enemy;
        }
    }
}