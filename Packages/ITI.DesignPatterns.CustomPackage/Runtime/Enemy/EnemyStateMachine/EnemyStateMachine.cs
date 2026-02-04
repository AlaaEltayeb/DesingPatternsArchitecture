using ITI.DesignPatterns.Foundation.Runtime.Event;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemy.EnemyStateMachine
{
    public sealed class EnemyStateMachine
    {
        private readonly IEventSystem _eventSystem;

        private EnemyStateId _currentStateId;

        public EnemyStateMachine(IEventSystem eventSystem)
        {
            _eventSystem = eventSystem;
        }

        public bool TryChangeState(EnemyStateId newState)
        {
            _currentStateId = newState;
            _eventSystem.Publish(new EnemyStateChanged(_currentStateId));
            return true;
        }
    }
}