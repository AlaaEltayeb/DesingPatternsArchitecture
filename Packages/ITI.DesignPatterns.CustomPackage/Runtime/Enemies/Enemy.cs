using ITI.DesignPatterns.CustomPackage.Runtime.Enemies.EnemiesBehaviour;
using ITI.DesignPatterns.CustomPackage.Runtime.GamePlay.Paths;
using ITI.DesignPatterns.CustomPackage.Runtime.Updates;
using ITI.DesignPatterns.Foundation.Runtime.Event;
using ITI.DesignPatterns.Foundation.Runtime.ViewBinding;
using System;
using System.Numerics;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies
{
    public sealed class Enemy : IDisposable
    {
        private readonly IEventSystem _eventSystem;
        private readonly IUpdateContext _updateContext;

        private readonly Path _enemyPath;
        private readonly EnemyData _enemyData;

        private int _hp;
        private int _pathIndex;

        private IEnemyStrategy _currentEnemyStrategy;

        public BindableProperty<Vector2> Position { get; } = new();
        public float Speed { get; private set; }

        public BindableProperty<bool> IsDeadOrReachedBase { get; private set; } = new();

        public Enemy(
            Path enemyPath,
            EnemyData enemyData,
            IEventSystem eventSystem,
            IUpdateContext updateContext)
        {
            _enemyPath = enemyPath;
            _enemyData = enemyData;
            _eventSystem = eventSystem;
            _updateContext = updateContext;

            _hp = enemyData.Hp;
            Speed = _enemyData.Speed;
            _updateContext.Add(OnUpdate);
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

            var targetPoint = _enemyPath.EnemyPath[_pathIndex].position;
            var newPosition = new Vector2(targetPoint.x, targetPoint.y);
            Position.Value = newPosition;
        }

        public void EnemyReachedDestination()
        {
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
            _currentEnemyStrategy.Dispose();
        }
    }
}