using ITI.DesignPatterns.CustomPackage.Runtime.Hud;
using ITI.DesignPatterns.CustomPackage.Runtime.TurretUi;
using ITI.DesignPatterns.Foundation.Runtime.Command;
using VContainer.Unity;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Startup
{
    public sealed class StartupInitializer : IInitializable
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public StartupInitializer(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void Initialize()
        {
            _commandDispatcher.RegisterAndExecute(() => new ShowHudCommand());
            _commandDispatcher.RegisterAndExecute(() => new ShowTurretsButtonsCommand());
        }
    }
}