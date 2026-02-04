namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public interface ITurretFactory
    {
        void SelectSlot(int index);
        void BuildTower(TowerType id);
    }
}