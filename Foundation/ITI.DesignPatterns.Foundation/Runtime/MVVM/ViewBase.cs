using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.Foundation.Runtime.MVVM
{
    public abstract class ViewBase<TViewModel> : MonoBehaviour, IView
        where TViewModel : IViewModel
    {
        protected TViewModel ViewModel { get; private set; }

        [Inject]
        private void InjectViewModelBase(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        private void Start()
        {
            Bind();
        }

        protected virtual void Bind()
        {
        }

        public IViewModel GetViewModel() => ViewModel;

        private void OnDestroy()
        {
            ViewModel.Dispose();
        }
    }
}