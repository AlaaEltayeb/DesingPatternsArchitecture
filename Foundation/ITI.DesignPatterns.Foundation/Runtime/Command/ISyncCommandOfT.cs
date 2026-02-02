namespace ITI.DesignPatterns.Foundation.Runtime.Command
{
    public interface ISyncCommand<out TResult> : ICommand
    {
        TResult Execute();
    }
}