public class GunnerTower : UglyTower
{
    protected override void Start()
    {
        Range = 4.0f;
        Rate = 0.25f;
        Damage = 7;
        Cost = 50;
    }
}