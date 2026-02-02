using System;
using System.Threading.Tasks;

namespace ITI.DesignPatterns.Foundation.Runtime.Command
{
    public interface ICommandDispatcher
    {
        void RegisterAndExecute(Func<ISyncCommand> command);
        Task Register(Func<IAsyncCommand> command);

        TResult RegisterAndExecute<TResult>(Func<ISyncCommand<TResult>> command);
        Task<TResult> Register<TResult>(Func<IAsyncCommand<TResult>> command);

        void AbortAll();
    }
}