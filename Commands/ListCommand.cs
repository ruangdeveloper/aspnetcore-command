
using Microsoft.Extensions.DependencyInjection;

namespace RuangDeveloper.AspNetCore.Command.Commands;

/// <summary>
/// List command.
/// </summary>
/// <param name="serviceProvider"></param>
public class ListCommand(IServiceProvider serviceProvider) : ICommand
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    /// <summary>
    /// Gets the name of the command.
    /// </summary>
    public string Name => "ls";
    /// <summary>
    /// Gets the description of the command.
    /// </summary>
    public string Description => "List all registered commands";

    /// <summary>
    /// Executes the command.
    /// </summary>
    /// <param name="args"></param>
    public void Execute(string[] args)
    {
        var registeredCommands = _serviceProvider.GetServices<ICommand>();
        Console.WriteLine("Registered Commands:");
        foreach (var command in registeredCommands)
        {
            Console.WriteLine($"- {command.Name} : {command.Description}");
        }
    }

    /// <summary>
    /// Executes the command asynchronously.
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public Task ExecuteAsync(string[] args)
    {
        return Task.CompletedTask;
    }
}