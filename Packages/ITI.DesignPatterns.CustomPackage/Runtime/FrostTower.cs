namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class FrostTower : UglyTower, ISlowable
    {
        protected override void Start()
        {
            Range = 3.0f;
            Rate = 0.9f;
            Damage = 22;
            Cost = 70;
        }

        protected override void Shoot(UglyEnemy target, int dmg)
        {
            target.TakeDamage(dmg);
            ApplySlow(target);
        }

        public void ApplySlow(UglyEnemy target)
        {
            target.ApplySlow(0.7f, 1.5f);
        }
    }
}