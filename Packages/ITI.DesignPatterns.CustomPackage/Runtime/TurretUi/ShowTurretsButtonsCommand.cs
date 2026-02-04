using ITI.DesignPatterns.Foundation.Runtime.Command;
using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime.TurretUi
{
    public sealed class ShowTurretsButtonsCommand : ISyncCommand
    {
        private IViewFactory _viewFactory;

        [Inject]
        private void Inject(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void Execute()
        {
            _viewFactory.Create<TurretBuilderView>(nameof(TurretBuilderView));
        }
    }
}