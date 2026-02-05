using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Enemies.EnemiesBehaviour
{
    public class EnemyFreezeStrategy : IEnemyStrategy
    {
        private readonly ISlowable _slowable;

        private CancellationTokenSource _cancellationTokenSource;

        public EnemyFreezeStrategy(ISlowable slowable)
        {
            _slowable = slowable;
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Execute(Enemy enemy)
        {
            ApplySlow(enemy);
            _ = RemoveSlowAsync(enemy);
        }

        private void ApplySlow(Enemy enemy)
        {
            enemy.UpdateSpeed(enemy.Speed * _slowable.SlowFactor);
        }

        private async Task RemoveSlowAsync(Enemy enemy)
        {
            await Task.Delay(TimeSpan.FromSeconds(_slowable.SlowDuration));

            if (_cancellationTokenSource.IsCancellationRequested)
                return;

            enemy.UpdateSpeed(enemy.Speed / _slowable.SlowFactor);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}