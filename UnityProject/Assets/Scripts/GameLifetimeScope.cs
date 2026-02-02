using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<IGameManager, GameManager>(Lifetime.Singleton);
        builder.Register<IEnemiesManager, EnemyManager>(Lifetime.Singleton);
        builder.Register<IUIManager, UIManager>(Lifetime.Singleton);
        builder.Register<IWaveManager, WaveManager>(Lifetime.Singleton);
        builder.Register<ITowerManager, TowerManager>(Lifetime.Singleton);
    }
}