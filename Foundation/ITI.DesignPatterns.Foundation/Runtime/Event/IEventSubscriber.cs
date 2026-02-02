namespace ITI.DesignPatterns.Foundation.Runtime.Event
{
    public interface IEventSubscriber
    {
        void Subscribe<TEvent>(EventDelegate<TEvent> action) where TEvent : IEvent;
        void Unsubscribe<TEvent>(EventDelegate<TEvent> action) where TEvent : IEvent;
        void UnsubscribeAll<TEvent>() where TEvent : IEvent;
    }
}