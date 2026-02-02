using ITI.DesignPatterns.Foundation.Runtime.Command;
using ITI.DesignPatterns.Foundation.Runtime.Event;
using VContainer;
using VContainer.Unity;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);

            builder.Register<IEventSystem, EventSystem>(Lifetime.Singleton);
            builder.Register<ICommandDispatcher, CommandDispatcher>(Lifetime.Singleton);
            builder.Register<ICommandFactory, CommandFactory>(Lifetime.Singleton);
        }
    }
}