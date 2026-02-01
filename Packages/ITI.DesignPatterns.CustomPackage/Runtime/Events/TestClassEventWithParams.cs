using ITI.DesignPatterns.Foundation.Runtime.Event;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Events
{
    public class TestClassEventWithParams : IEvent
    {
        public int Number { get; }

        public TestClassEventWithParams(int number)
        {
            Number = number;
        }
    }
}