
using Microsoft.Extensions.DependencyInjection;

namespace RuangDeveloper.AspNetCore.Command.Commands;

public class ListCommand(IServiceProvider serviceProvider) : ICommand
{
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    public string Name => "ls";
    public string Description => "List all registered commands";

    public void Execute(string[] args)
    {
        var registeredCommands = _serviceProvider.GetServices<ICommand>();
        Console.WriteLine("Registered Commands:");
        foreach (var command in registeredCommands)
        {
            Console.WriteLine($"- {command.Name} : {command.Description}");
        }
    }

    public Task ExecuteAsync(string[] args)
    {
        return Task.CompletedTask;
    }
}