using ITI.DesignPatterns.Foundation.Runtime.Event;
using VContainer;
using VContainer.Unity;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class GameLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IEventSystem, EventSystem>(Lifetime.Singleton);

            builder.Register<IGameManager, GameManager>(Lifetime.Singleton);
            builder.Register<IEnemiesManager, EnemyManager>(Lifetime.Singleton);
            builder.Register<IUIManager, UIManager>(Lifetime.Singleton);
            builder.Register<IWaveManager, WaveManager>(Lifetime.Singleton);
            builder.Register<ITowerManager, TowerManager>(Lifetime.Singleton);
        }
    }
}