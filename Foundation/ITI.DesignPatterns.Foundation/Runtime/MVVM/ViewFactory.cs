#nullable enable
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ITI.DesignPatterns.Foundation.Runtime.MVVM
{
    public sealed class ViewFactory : IViewFactory
    {
        private readonly IViewContainer _viewContainer;
        private readonly IObjectResolver _objectResolver;

        public ViewFactory(IViewContainer viewContainer, IObjectResolver objectResolver)
        {
            _viewContainer = viewContainer;
            _objectResolver = objectResolver;
        }

        public void Create<TView>(string name, Transform parent = null) where TView : IView
        {
            var prefab = _viewContainer.GetView<TView>();

            if (prefab == null)
                throw new Exception($"A prefab with type '{typeof(TView)}' must be assigned at the view container so");

            CreateView(
                prefab,
                name,
                parent);
        }

        private void CreateView(GameObject prefab, string name, Transform parent)
        {
            GameObject? gameObject;
            try
            {
                gameObject = _objectResolver.Instantiate(prefab, parent);
                gameObject.SetActive(false);
            }
            catch (Exception e)
            {
                throw new Exception($"Failed to Instantiate view '{name}'");
            }

            gameObject.name = name;
            var view = gameObject.GetComponent<IView>();
            var viewModel = view.GetViewModel();

            if (viewModel == null)
                throw new Exception($"ViewModel is not assigned to the view '{name}'");

            gameObject.SetActive(true);
        }
    }
}