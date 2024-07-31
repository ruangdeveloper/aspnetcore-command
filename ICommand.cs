namespace RuangDeveloper.AspNetCore.Command;

/// <summary>
/// Represents a command.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Gets the name of the command.
    /// </summary>
    string Name { get; }
    /// <summary>
    /// Gets the description of the command.
    /// </summary>
    string Description { get; }
    /// <summary>
    /// Executes the command asynchronously.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    Task ExecuteAsync(string[] args);
    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="args"></param>
    void Execute(string[] args);
}