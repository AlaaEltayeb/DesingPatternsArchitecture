using ITI.DesignPatterns.CustomPackage.Runtime.Enemies;
using ITI.DesignPatterns.CustomPackage.Runtime.GamePlay;
using ITI.DesignPatterns.CustomPackage.Runtime.GamePlay.Paths;
using ITI.DesignPatterns.CustomPackage.Runtime.Hud;
using ITI.DesignPatterns.CustomPackage.Runtime.Hud.TurretUi;
using ITI.DesignPatterns.CustomPackage.Runtime.Startup;
using ITI.DesignPatterns.CustomPackage.Runtime.Strategy;
using ITI.DesignPatterns.CustomPackage.Runtime.Turrets;
using ITI.DesignPatterns.CustomPackage.Runtime.Updates;
using ITI.DesignPatterns.Foundation.Runtime.AssetManagement;
using ITI.DesignPatterns.Foundation.Runtime.Command;
using ITI.DesignPatterns.Foundation.Runtime.Event;
using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using ITI.DesignPatterns.Foundation.Runtime.VContainer;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ITI.DesignPatterns.CustomPackage.Runtime
{
    public class GameLifeTimeScope : LifetimeScope
    {
        [SerializeField]
        private TurretContainer _turretContainer;

        [SerializeField]
        private ViewContainer _viewContainer;

        [SerializeField]
        private EnemyContainer _enemyContainer;

        [SerializeField]
        private AssetCatalog _assetCatalog;

        [SerializeField]
        private Path _enemyPath;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<StartupInitializer>();

            builder.Register<IGameManager, GameManager>(Lifetime.Singleton);

            builder.Register<IEventSystem, EventSystem>(Lifetime.Singleton);
            builder.Register<ICommandDispatcher, CommandDispatcher>(Lifetime.Singleton);
            builder.Register<ICommandFactory, CommandFactory>(Lifetime.Singleton);

            builder.RegisterInstance<IViewContainer>(_viewContainer);
            builder.Register<IViewFactory, ViewFactory>(Lifetime.Singleton);
            builder.Register<GameDataModel>(Lifetime.Singleton);

            builder.RegisterInstance<ITurretContainer>(_turretContainer);
            builder.Register<ITurretProvider, TurretProvider>(Lifetime.Singleton);
            builder.Register<ITurretFactory, TurretFactory>(Lifetime.Singleton);

            builder.Register<IAttackStrategyProvider, AttackStrategyProvider>(Lifetime.Singleton);

            builder.RegisterInstance<IEnemyContainer>(_enemyContainer);
            builder.Register<IEnemyProvider, EnemyProvider>(Lifetime.Singleton);
            builder.Register<IEnemyFactory, EnemyFactory>(Lifetime.Singleton);

            builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
            builder.RegisterInstance<IAssetCatalog>(_assetCatalog);

            builder.RegisterInstance(_enemyPath);

            builder.RegisterComponentOnNewGameObject<UpdateContext>(Lifetime.Singleton)
                .AsImplementedInterfaces();

            RegisterViewAndViewModels(builder);
        }

        private void RegisterViewAndViewModels(IContainerBuilder builder)
        {
            builder.RegisterViewWithViewModelOnNewObject<HudView, HudViewModel>(Lifetime.Transient);
            builder.RegisterViewWithViewModelOnNewObject<TurretBuilderView, TurretBuilderViewModel>(Lifetime.Transient);
        }
    }
}