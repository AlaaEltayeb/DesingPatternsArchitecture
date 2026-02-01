using ITI.DesignPatterns.Foundation.Runtime.Event;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Events
{
    public struct TestEventWithParams : IEvent
    {
        public int Number { get; }

        public TestEventWithParams(int number)
        {
            Number = number;
        }
    }
}