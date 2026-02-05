using ITI.DesignPatterns.Foundation.Runtime.Command;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public class SpawnEnemyCommand : IAsyncCommand
    {
        private IEnemyFactory _enemyFactory;

        private readonly EnemyType _enemyType;

        [Inject]
        private void Inject(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        public SpawnEnemyCommand(EnemyType enemyType)
        {
            _enemyType = enemyType;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken) =>
            await _enemyFactory.CreateEnemy(_enemyType.ToString(), _enemyType, Vector2.zero);
    }
}