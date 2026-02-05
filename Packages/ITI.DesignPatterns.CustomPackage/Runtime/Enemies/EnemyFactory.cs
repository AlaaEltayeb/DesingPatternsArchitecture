using ITI.DesignPatterns.CustomPackage.Runtime.GamePlay.Paths;
using ITI.DesignPatterns.CustomPackage.Runtime.Updates;
using ITI.DesignPatterns.Foundation.Runtime.AssetManagement;
using ITI.DesignPatterns.Foundation.Runtime.Event;
using System;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public sealed class EnemyFactory : IEnemyFactory
    {
        private readonly IEnemyProvider _enemyProvider;
        private readonly IAssetProvider _assetProvider;
        private readonly IObjectResolver _objectResolver;

        //Replace Later With A Better Solution
        private readonly IEventSystem _eventSystem;
        private readonly IUpdateContext _updateContext;

        public EnemyFactory(IEnemyProvider enemyProvider, IAssetProvider assetProvider, IObjectResolver objectResolver)
        {
            _enemyProvider = enemyProvider;
            _assetProvider = assetProvider;
            _objectResolver = objectResolver;
        }

        public async Task CreateEnemy(
            string name,
            EnemyType enemyType,
            Vector2 position,
            Transform parent = null)
        {
            if (!_enemyProvider.TryGetEnemy(enemyType, out var enemyData))
                throw new Exception($"Enemy with type '{enemyType}' is not found in the enemy provider");

            var enemyPath = new Path();

            var enemyPrefabId = _enemyProvider.GetEnemyPrefabId();
            var enemyPrefab = await _assetProvider.GetPrefab(enemyPrefabId);
            var enemyGameObject = _objectResolver.Instantiate(enemyPrefab, parent);
            var enemyView = enemyGameObject.GetComponent<EnemyView>();

            var enemy = new Enemy(enemyPath, enemyData);
            _objectResolver.Inject(enemy);

            enemyView.SetEnemy(enemy);

            enemyGameObject.name = name;

            enemyGameObject.transform.position = new Vector3(position.x, position.y, 0);
        }
    }
}