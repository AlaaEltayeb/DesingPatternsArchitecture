using System.Threading;
using System.Threading.Tasks;

namespace ITI.DesignPatterns.Foundation.Runtime.Command
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(CancellationToken cancellationToken);
    }
}