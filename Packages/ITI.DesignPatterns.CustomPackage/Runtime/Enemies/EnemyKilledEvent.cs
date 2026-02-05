using ITI.DesignPatterns.Foundation.Runtime.Event;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public struct EnemyKilledEvent : IEvent
    {
        public int GoldReward { get; private set; }

        public EnemyKilledEvent(int goldReward)
        {
            GoldReward = goldReward;
        }
    }
}