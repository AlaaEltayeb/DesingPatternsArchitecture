using UnityEngine;

namespace ITI.DesignPatterns.Foundation.Runtime.MVVM
{
    public interface IViewFactory
    {
        void Create<TView>(
            string name,
            Transform parent = null)
            where TView : IView;
    }
}