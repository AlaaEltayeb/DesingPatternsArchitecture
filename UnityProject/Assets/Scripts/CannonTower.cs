public class CannonTower : UglyTower
{
    protected override void Start()
    {
        Range = 3.5f;
        Rate = 0.6f;
        Damage = 6;
        Cost = 80;
    }
}