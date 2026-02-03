using System.Threading;

namespace ITI.DesignPatterns.Foundation.Runtime.MVVM
{
    public abstract class ViewModelBase : IViewModel
    {
        public CancellationToken CancellationToken => ApplicationLifeTimeCancellationToken.Token;

        public virtual void Dispose()
        {
        }
    }
}