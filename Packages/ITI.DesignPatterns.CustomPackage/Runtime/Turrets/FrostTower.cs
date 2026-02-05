using ITI.DesignPatterns.CustomPackage.Runtime.Enemies;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class FrostTower : UglyTower, ISlowable
    {
        public float SlowFactor { get; } = 0.7f;
        public float SlowDuration { get; } = 1.5f;

        protected override void Shoot(Enemy target, int dmg)
        {
            //Execute Command
        }
    }
}