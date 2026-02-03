using ITI.DesignPatterns.Foundation.Runtime.Command;
using ITI.DesignPatterns.Foundation.Runtime.MVVM;
using VContainer;

namespace ITI.DesignPatterns.CustomPackage.Runtime.New_Folder
{
    public sealed class SpawnTestViewCommand : ISyncCommand
    {
        private IViewFactory _viewFactory;

        [Inject]
        private void Inject(IViewFactory viewFactory)
        {
            _viewFactory = viewFactory;
        }

        public void Execute()
        {
            _viewFactory.Create<TestView>("TestView");
        }
    }
}