using ITI.DesignPatterns.Foundation.Runtime.Event;
using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Events
{
    public class EventPublisher : MonoBehaviour
    {
        [Inject]
        private IEventSystem _eventSystem;

        private void Start()
        {
            _eventSystem.Publish(new TestEvent());
            _eventSystem.Publish(new TestEventWithParams());
            _eventSystem.Publish(new TestClassEventWithParams(45));
        }
    }
}