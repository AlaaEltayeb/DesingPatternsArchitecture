using ITI.DesignPatterns.Foundation.Runtime.Event;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField]
        private TurretContainer _turretContainer;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IEventSystem, EventSystem>(Lifetime.Singleton);

            builder.Register<IGameManager, GameManager>(Lifetime.Singleton);
            builder.Register<IEnemiesManager, EnemyManager>(Lifetime.Singleton);
            builder.Register<IUIManager, UIManager>(Lifetime.Singleton);
            builder.Register<IWaveManager, WaveManager>(Lifetime.Singleton);
            builder.Register<ITurretFactory, TurretFactory>(Lifetime.Singleton);

            builder.RegisterInstance<ITurretContainer>(_turretContainer);
            builder.Register<ITurretProvider, TurretProvider>(Lifetime.Singleton);
        }
    }
}