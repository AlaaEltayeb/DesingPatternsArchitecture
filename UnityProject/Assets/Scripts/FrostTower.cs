public class FrostTower : UglyTower
{
    protected override void Start()
    {
        Range = 3.0f;
        Rate = 0.9f;
        Damage = 22;
        Cost = 70;
    }
}