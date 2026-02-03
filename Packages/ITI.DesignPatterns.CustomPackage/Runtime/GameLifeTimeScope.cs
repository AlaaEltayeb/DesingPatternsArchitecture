using ITI.DesignPatterns.CustomPackage.Runtime.New_Folder;
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

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<StartupInitializer>();

            builder.Register<IEventSystem, EventSystem>(Lifetime.Singleton);
            builder.Register<ICommandDispatcher, CommandDispatcher>(Lifetime.Singleton);
            builder.Register<ICommandFactory, CommandFactory>(Lifetime.Singleton);

            //builder.Register<IGameManager, GameManager>(Lifetime.Singleton);
            //builder.Register<IEnemiesManager, EnemyManager>(Lifetime.Singleton);
            //builder.Register<IUIManager, UIManager>(Lifetime.Singleton);
            //builder.Register<IWaveManager, WaveManager>(Lifetime.Singleton);
            //builder.Register<ITurretFactory, TurretFactory>(Lifetime.Singleton);

            //builder.RegisterInstance<ITurretContainer>(_turretContainer);
            //builder.Register<ITurretProvider, TurretProvider>(Lifetime.Singleton);

            builder.RegisterInstance<IViewContainer>(_viewContainer);
            builder.Register<IViewFactory, ViewFactory>(Lifetime.Singleton);

            builder.Register<PlayerDataModel>(Lifetime.Singleton);

            RegisterViewAndViewModels(builder);
        }

        private void RegisterViewAndViewModels(IContainerBuilder builder)
        {
            builder.RegisterViewWithViewModelOnNewObject<TestView, TestViewModel>(Lifetime.Transient);

            builder.RegisterViewWithViewModelOnNewObject<NameSetterView, NameSetterViewModel>(Lifetime.Transient);
        }
    }
}