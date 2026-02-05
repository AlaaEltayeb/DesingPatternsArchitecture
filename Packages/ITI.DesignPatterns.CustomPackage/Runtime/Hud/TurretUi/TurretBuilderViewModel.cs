using ITI.DesignPatterns.CustomPackage.Runtime.Enemies;
using ITI.DesignPatterns.CustomPackage.Runtime.Turrets;
using ITI.DesignPatterns.Foundation.Runtime.Command;
using ITI.DesignPatterns.Foundation.Runtime.MVVM;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Hud.TurretUi
{
    public sealed class TurretBuilderViewModel : ViewModelBase
    {
        private readonly ICommandDispatcher _commandDispatcher;

        public TurretBuilderViewModel(ICommandDispatcher commandDispatcher)
        {
            _commandDispatcher = commandDispatcher;
        }

        public void BuildTurret(TowerType turretType)
        {
            _commandDispatcher.RegisterAndExecute(() => new SpawnTowerCommand(turretType));
        }

        public void SpawnEnemy(EnemyType enemyType)
        {
            _commandDispatcher.RegisterAndExecuteAsync(() => new SpawnEnemyCommand(enemyType));
        }
    }
}