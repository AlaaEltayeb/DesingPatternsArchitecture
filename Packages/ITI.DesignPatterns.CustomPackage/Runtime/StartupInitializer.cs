using ITI.DesignPatterns.Foundation.Runtime.Command;
using VContainer.Unity;

namespace ITI.DesignPatterns.CustomPackage.Runtime.New_Folder
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
            _commandDispatcher.RegisterAndExecute(() => new SpawnTestViewCommand());
        }
    }
}