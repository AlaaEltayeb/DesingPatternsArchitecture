using System.Collections.Generic;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public sealed class EnemyProvider : IEnemyProvider
    {
        private readonly IEnemyContainer _enemyContainer;

        private readonly Dictionary<EnemyType, EnemyData> _mapping = new();

        public EnemyProvider(IEnemyContainer enemyContainer)
        {
            _enemyContainer = enemyContainer;
            MapEnemies();
        }

        private void MapEnemies()
        {
            foreach (var enemyData in _enemyContainer.Enemies)
            {
                _mapping.Add(enemyData.EnemyType, enemyData);
            }
        }

        public bool TryGetEnemy(EnemyType enemyType, out EnemyData enemy)
        {
            enemy = null;

            if (!_mapping.TryGetValue(enemyType, out var value))
                return false;

            enemy = value;
            return true;
        }

        public string GetEnemyPrefabId() => _enemyContainer.EnemyPrefabId;
    }
}