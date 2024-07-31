namespace RuangDeveloper.AspNetCore.Command;

/// <summary>
/// Represents the configuration for commands.
/// </summary>
public class CommandConfiguration
{
    /// <summary>
    /// Gets the commands that are registered.
    /// </summary>
    public IEnumerable<Type> Commands { get; private set; } = [];
    /// <summary>
    /// Gets the command call identifier.
    /// </summary>
    public string CommandCallIdentifier { get; private set; } = "command";

    /// <summary>
    /// Adds a command to the configuration.
    /// </summary>
    /// <typeparam name="TCommand"></typeparam>
    /// <returns></returns>
    public CommandConfiguration AddCommand<TCommand>() where TCommand : ICommand
    {
        Commands = Commands.Append(typeof(TCommand));

        return this;
    }

    /// <summary>
    /// Sets the command call identifier.
    /// </summary>
    /// <param name="commandCallIdentifier"></param>
    /// <returns></returns>
    public CommandConfiguration SetCommandCallIdentifier(string commandCallIdentifier)
    {
        CommandCallIdentifier = commandCallIdentifier;

        return this;
    }
}