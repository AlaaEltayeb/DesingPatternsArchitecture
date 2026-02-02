namespace ITI.DesignPatterns.Foundation.Runtime.Command
{
    public interface ICommandFactory
    {
        void Populate<TCommand>(TCommand command) where TCommand : ICommand;
    }
}