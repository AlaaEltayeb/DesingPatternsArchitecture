namespace ITI.DesignPatterns.Foundation.Runtime.Event
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(in TEvent sendEvent) where TEvent : IEvent;
    }
}