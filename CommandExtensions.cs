using Microsoft.Extensions.DependencyInjection;
using RuangDeveloper.AspNetCore.Command.Commands;

namespace RuangDeveloper.AspNetCore.Command;

/// <summary>
/// Command extensions for the service collection.
/// </summary>
public static class CommandExtensions
{
    /// <summary>
    /// Adds the commands to the service collection.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
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