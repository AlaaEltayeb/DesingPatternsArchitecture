using ITI.DesignPatterns.CustomPackage.Runtime.GamePlay;
using ITI.DesignPatterns.CustomPackage.Runtime.Hud;
using ITI.DesignPatterns.CustomPackage.Runtime.Hud.TurretUi;
using ITI.DesignPatterns.Foundation.Runtime.Command;
using VContainer.Unity;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Startup
{
    public sealed class StartupInitializer : IInitializable
    {
        private readonly IGameManager _gameManager;
        private readonly ICommandDispatcher _commandDispatcher;

        public StartupInitializer(IGameManager gameManager, ICommandDispatcher commandDispatcher)
        {
            _gameManager = gameManager;
            _commandDispatcher = commandDispatcher;
        }

        public void Initialize()
        {
            _gameManager.Init();
            _commandDispatcher.RegisterAndExecute(() => new ShowHudCommand());
            _commandDispatcher.RegisterAndExecute(() => new ShowTurretsButtonsCommand());
        }
    }
}