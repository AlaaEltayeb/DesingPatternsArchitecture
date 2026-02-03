using UnityEngine;
using VContainer;

namespace ITI.DesignPatterns.Foundation.Runtime.MVVM
{
    public sealed class ViewFactory : IViewFactory
    {
        private readonly IObjectResolver _objectResolver;

        public ViewFactory(IObjectResolver objectResolver)
        {
            _objectResolver = objectResolver;
        }

        public void Create<TView>(string name, Transform parent = null) where TView : IView
        {
        }
    }
}