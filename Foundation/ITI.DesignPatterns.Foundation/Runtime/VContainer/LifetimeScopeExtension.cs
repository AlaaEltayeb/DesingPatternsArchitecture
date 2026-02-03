using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using VContainer;
using VContainer.Unity;

namespace ITI.DesignPatterns.Foundation.Runtime.VContainer
{
    public static class LifetimeScopeExtension
    {
        public static void RegisterViewWithViewModelOnNewObject<TView, TViewModel>(
            this IContainerBuilder builder,
            Lifetime lifetime,
            string newObjectName = null)
            where TViewModel : class, IViewModel
            where TView : ViewBase<TViewModel>, IView
        {
            builder.RegisterComponentOnNewGameObject<TView>(lifetime, newObjectName);
            builder.Register<TViewModel>(lifetime);
        }

        public static void RegisterComponentsInHierarchy<TView, TViewModel>(
            this IContainerBuilder builder,
            Lifetime lifetime)
            where TViewModel : class, IViewModel
            where TView : ViewBase<TViewModel>, IView
        {
            builder.RegisterComponentInHierarchy<TView>();
            builder.Register<TViewModel>(lifetime);
        }
    }
}