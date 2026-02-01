using ITI.DesignPatterns.Foundation.Runtime.Event;
using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Events
{
    public class EventSubscriber : MonoBehaviour
    {
        [Inject]
        private IEventSystem _eventSystem;

        private void Awake()
        {
            _eventSystem.Subscribe<TestEvent>(OnTestEvent);
            _eventSystem.Subscribe<TestEventWithParams>(OnTestEventWithParams);
            _eventSystem.Subscribe<TestClassEventWithParams>(OnTestClassEventWithParams);
        }

        private void OnTestClassEventWithParams(TestClassEventWithParams evt)
        {
            Debug.Log($"With Param Class Event: {evt.Number}");
        }

        private void OnTestEventWithParams(TestEventWithParams evt)
        {
            Debug.Log($"With Param Struct Event: {evt.Number}");
        }

        private void OnTestEvent(TestEvent evt)
        {
            Debug.Log("Paramless Event");
        }

        private void OnDestroy()
        {
            _eventSystem.Unsubscribe<TestEvent>(OnTestEvent);
            _eventSystem.Unsubscribe<TestEventWithParams>(OnTestEventWithParams);
            _eventSystem.Unsubscribe<TestClassEventWithParams>(OnTestClassEventWithParams);
        }
    }
}