using ITI.DesignPatterns.Foundation.Runtime.Event;
using System;
using System.Collections.Generic;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public sealed class EventSystem : IEventSystem
    {
        private readonly Dictionary<Type, List<object>> _handlersAction = new();

        public void Subscribe<TEvent>(EventDelegate<TEvent> action) where TEvent : IEvent
        {
            var type = typeof(TEvent);

            if (!_handlersAction.ContainsKey(type))
                _handlersAction.Add(type, new List<object>());

            _handlersAction[type].Add(action);
        }

        public void Unsubscribe<TEvent>(EventDelegate<TEvent> action) where TEvent : IEvent
        {
            if (!_handlersAction.TryGetValue(typeof(TEvent), out var actions))
                return;

            actions.Remove(action);

            if (actions.Count == 0)
                UnsubscribeAll<TEvent>();
        }

        public void UnsubscribeAll<TEvent>() where TEvent : IEvent
        {
            if (!_handlersAction.ContainsKey(typeof(TEvent)))
                return;

            _handlersAction.Remove(typeof(TEvent));
        }

        public void Publish<TEvent>(in TEvent sendEvent) where TEvent : IEvent
        {
            if (!_handlersAction.TryGetValue(typeof(TEvent), out var actions))
                return;

            var snapshot = actions.ToArray();

            foreach (EventDelegate<TEvent> action in snapshot)
            {
                action(sendEvent);
            }
        }
    }
}