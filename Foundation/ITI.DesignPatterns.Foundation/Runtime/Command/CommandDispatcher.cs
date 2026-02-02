using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITI.DesignPatterns.Foundation.Runtime.Command
{
    public sealed class CommandDispatcher : ICommandDispatcher, IDisposable
    {
        private readonly ICommandFactory _commandFactory;
        private CancellationTokenSource _cancellationTokenSource;

        public CommandDispatcher(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;
            RefreshCancellationToken();
        }

        ~CommandDispatcher()
        {
            Dispose();
        }

        public void RegisterAndExecute(Func<ISyncCommand> command)
        {
            var commandToExecute = command.Invoke();

            try
            {
                _commandFactory.Populate(commandToExecute);
            }
            catch (Exception e)
            {
                return;
            }

            commandToExecute.Execute();
        }

        public async Task Register(Func<IAsyncCommand> command)
        {
            var commandToExecute = command.Invoke();

            try
            {
                _commandFactory.Populate(commandToExecute);
            }
            catch (Exception e)
            {
                return;
            }

            await commandToExecute.ExecuteAsync(_cancellationTokenSource.Token);
        }

        public TResult RegisterAndExecute<TResult>(Func<ISyncCommand<TResult>> command)
        {
            var commandToExecute = command.Invoke();

            try
            {
                _commandFactory.Populate(commandToExecute);
            }
            catch (Exception e)
            {
                return default;
            }

            return commandToExecute.Execute();
        }

        public async Task<TResult> Register<TResult>(Func<IAsyncCommand<TResult>> command)
        {
            var commandToExecute = command.Invoke();

            try
            {
                _commandFactory.Populate(commandToExecute);
            }
            catch (Exception e)
            {
                return default;
            }

            var result = await commandToExecute.ExecuteAsync(_cancellationTokenSource.Token);

            return result;
        }

        public void AbortAll()
        {
            RefreshCancellationToken();
        }

        private void RefreshCancellationToken()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();

            GC.SuppressFinalize(this);
        }
    }
}