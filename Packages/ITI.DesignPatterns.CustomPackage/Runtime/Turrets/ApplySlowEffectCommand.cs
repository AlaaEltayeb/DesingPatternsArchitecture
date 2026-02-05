using ITI.DesignPatterns.CustomPackage.Runtime.Enemies;
using ITI.DesignPatterns.CustomPackage.Runtime.Enemies.EnemiesBehaviour;
using ITI.DesignPatterns.Foundation.Runtime.Command;

namespace ITI.DesignPatterns.CustomPackage.Runtime.Turrets
{
    public sealed class ApplySlowEffectCommand : ISyncCommand
    {
        private readonly int _damage;
        private readonly Enemy _enemy;
        private readonly ISlowable _slowable;

        public ApplySlowEffectCommand(Enemy enemy, int damage, ISlowable slowable)
        {
            _damage = damage;
            _enemy = enemy;
            _slowable = slowable;
        }

        public void Execute()
        {
            _enemy.TakeDamage(_damage, new EnemyFreezeStrategy(_slowable));
        }
    }
}