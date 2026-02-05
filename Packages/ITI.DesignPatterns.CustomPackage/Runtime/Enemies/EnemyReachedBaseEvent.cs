using ITI.DesignPatterns.Foundation.Runtime.Event;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public struct EnemyReachedBaseEvent : IEvent
    {
        public int EnemyDamageToBase { get; }

        public EnemyReachedBaseEvent(int enemyDamageToBase)
        {
            EnemyDamageToBase = enemyDamageToBase;
        }
    }
}