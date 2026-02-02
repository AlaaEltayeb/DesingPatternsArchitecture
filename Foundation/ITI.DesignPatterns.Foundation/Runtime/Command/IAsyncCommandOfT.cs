using System.Threading;
using System.Threading.Tasks;

namespace ITI.DesignPatterns.Foundation.Runtime.Command
{
    public interface IAsyncCommand<TResult> : ICommand
    {
        Task<TResult> ExecuteAsync(CancellationToken cancellationToken);
    }
}