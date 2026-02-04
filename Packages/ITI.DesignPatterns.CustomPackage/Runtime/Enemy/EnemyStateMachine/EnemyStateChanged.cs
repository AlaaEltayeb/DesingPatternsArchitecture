using ITI.DesignPatterns.Foundation.Runtime.Event;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemy.EnemyStateMachine
{
    public struct EnemyStateChanged : IEvent
    {
        public EnemyStateId EnemyState { get; }

        public EnemyStateChanged(EnemyStateId enemyState)
        {
            EnemyState = enemyState;
        }
    }
}