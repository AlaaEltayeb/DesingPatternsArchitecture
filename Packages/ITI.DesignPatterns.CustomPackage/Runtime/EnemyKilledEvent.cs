using ITI.DesignPatterns.Foundation.Runtime.Event;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public struct EnemyKilledEvent : IEvent
    {
        public UglyEnemy Enemy;

        public EnemyKilledEvent(UglyEnemy enemy)
        {
            Enemy = enemy;
        }
    }
}