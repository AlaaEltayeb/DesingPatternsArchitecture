using UnityEngine;

namespace ITI.DesignPatterns.Foundation.Runtime.MVVM
{
    public interface IViewContainer
    {
        GameObject GetView<TView>();
    }
}