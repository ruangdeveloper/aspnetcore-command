using Microsoft.Extensions.DependencyInjection;
using RuangDeveloper.AspNetCore.Command.Commands;

namespace RuangDeveloper.AspNetCore.Command;

public static class CommandExtensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services, Action<CommandConfiguration> configure)
    {
        var commandConfiguration = new CommandConfiguration();

        configure.Invoke(commandConfiguration);

        services.AddSingleton(commandConfiguration);
        services.AddTransient<ICommand, ListCommand>();

        foreach (var command in commandConfiguration.Commands)
        {
            services.AddTransient(typeof(ICommand), command);
        }

        return services;
    }
}