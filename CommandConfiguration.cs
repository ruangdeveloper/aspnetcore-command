namespace RuangDeveloper.AspNetCore.Command;

public class CommandConfiguration
{
    public IEnumerable<Type> Commands { get; private set; } = [];
    public string CommandCallIdentifier { get; private set; } = "command";

    public CommandConfiguration AddCommand<TCommand>() where TCommand : ICommand
    {
        Commands = Commands.Append(typeof(TCommand));

        return this;
    }

    public CommandConfiguration SetCommandCallIdentifier(string commandCallIdentifier)
    {
        CommandCallIdentifier = commandCallIdentifier;

        return this;
    }
}