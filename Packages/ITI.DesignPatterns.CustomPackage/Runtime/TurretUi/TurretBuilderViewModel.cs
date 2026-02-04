using ITI.DesignPatterns.Foundation.Runtime.Command;
using ITI.DesignPatterns.Foundation.Runtime.MVVM;

namespace ITI.DesignPatterns.CustomPackage.Runtime.TurretUi
{
    public sealed class TurretBuilderViewModel : ViewModelBase
    {
        private ICommandDispatcher _commandDispatcher;

        public TurretBuilderViewModel(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void BuildTurret(TowerType turretType)
        {
            _commandDispatcher.RegisterAndExecute(() => new SpawnTowerCommand(turretType));
        }
    }
}