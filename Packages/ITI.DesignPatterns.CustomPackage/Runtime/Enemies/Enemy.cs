using ITI.DesignPatterns.CustomPackage.Runtime.Enemies.EnemiesBehaviour;
using ITI.DesignPatterns.CustomPackage.Runtime.GamePlay.Paths;
using ITI.DesignPatterns.CustomPackage.Runtime.Updates;
using ITI.DesignPatterns.Foundation.Runtime.AssetManagement;
using ITI.DesignPatterns.Foundation.Runtime.Event;
using ITI.DesignPatterns.Foundation.Runtime.ViewBinding;
using System;
using System.Threading.Tasks;
using UnityEngine;
using VContainer;
using Vector2 = System.Numerics.Vector2;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public sealed class Enemy : IDisposable
    {
        private IEventSystem _eventSystem;
        private IUpdateContext _updateContext;
        private IAssetProvider _assetProvider;

        private Path _enemyPath;
        private readonly EnemyData _enemyData;

        private int _hp;
        private int _pathIndex;

        private IEnemyStrategy _currentEnemyStrategy;

        public float Speed { get; private set; }

        public BindableProperty<Vector2> Position { get; } = new();
        public BindableProperty<Vector2> InitialPosition { get; } = new();
        public BindableProperty<bool> IsDeadOrReachedBase { get; private set; } = new();
        public BindableProperty<Sprite> EnemyImage { get; private set; } = new();

        public Enemy(EnemyData enemyData)
        {
            _enemyData = enemyData;
        }

        [Inject]
        private void Inject(
            Path enemyPath,
            IEventSystem eventSystem,
            IUpdateContext updateContext,
            IAssetProvider assetProvider)
        {
            _enemyPath = enemyPath;

            _eventSystem = eventSystem;
            _updateContext = updateContext;
            _assetProvider = assetProvider;

            _hp = _enemyData.Hp;
            Speed = _enemyData.Speed;

            InitialPosition.Value = new Vector2(_enemyPath.EnemyPath[_pathIndex].x, _enemyPath.EnemyPath[_pathIndex].y);
            _updateContext.Add(OnUpdate);

            _ = GetEnemyImage();
        }

        private async Task GetEnemyImage()
        {
            var result = await _assetProvider.GetImage(_enemyData.EnemyImageId.ToString());

            EnemyImage.Value = result;
        }

        public void UpdateSpeed(float newValue)
        {
            Speed = newValue;
        }

        private void OnUpdate()
        {
            if (_enemyData.Hp <= 0)
                return;

            if (_enemyPath == null || _enemyPath.EnemyPath.Count == 0)
                return;

            if (IsDeadOrReachedBase.Value)
                return;

            var targetPoint = _enemyPath.EnemyPath[_pathIndex];
            var newPosition = new Vector2(targetPoint.x, targetPoint.y);
            Position.Value = newPosition;
        }

        public void EnemyReachedDestination()
        {
            if (_enemyData.Hp <= 0)
                return;

            if (_enemyPath == null || _enemyPath.EnemyPath.Count <= 0)
                return;

            if (IsDeadOrReachedBase.Value)
                return;

            _pathIndex++;
            if (_pathIndex >= _enemyPath.EnemyPath.Count)
            {
                _eventSystem.Publish(new EnemyReachedBaseEvent(_enemyData.DamageToBase));
                IsDeadOrReachedBase.Value = true;
            }
        }

        public void TakeDamage(int dmg, IEnemyStrategy enemyStrategy)
        {
            _hp -= dmg;

            if (_hp <= 0)
            {
                _eventSystem.Publish(new EnemyKilledEvent(_enemyData.GoldReward));
                IsDeadOrReachedBase.Value = true;
                return;
            }

            _currentEnemyStrategy = enemyStrategy;
            enemyStrategy.Execute(this);
        }

        public void Dispose()
        {
            _updateContext.Remove(OnUpdate);
            _currentEnemyStrategy?.Dispose();
        }
    }
}