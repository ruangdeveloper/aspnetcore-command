using System.Text.Json;
using System.Text.Json.Serialization;
using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RuangDeveloper.AspNetCore.Command
{
    public class Options
    {
        [Option("command", Required = true, HelpText = "Command to execute")]
        public string? Command { get; set; }

        [Option("args", Required = false, HelpText = "Arguments for the command")]
        public IEnumerable<string>? Args { get; set; }
    }

    public static class CommandRunner
    {
        public static void RunWithCommands(this IHost host, string[] args)
        {
            if (args.Length >= 1)
            {
                var commandConfiguration = host.Services.GetRequiredService<CommandConfiguration>();
                var commandServices = host.Services.GetServices<ICommand>();
                var commandIndex = args[0] == "run" ? 1 : 0;
                var commandCall = args[commandIndex];

                if (commandCall.Equals(commandConfiguration.CommandCallIdentifier))
                {
                    // Get all arguments after the "command" index
                    var commandArgs = new List<string>();
                    for (var i = commandIndex; i < args.Length; i++)
                    {
                        commandArgs.Add(args[i]);
                    }

                    Parser.Default.ParseArguments<Options>(args)
                        .WithParsed(options =>
                        {
                            try
                            {
                                var command = commandServices.FirstOrDefault(t => t.Name == options.Command);
                                if (command != null)
                                {
                                    var arguments = new List<string>();
                                    if (options.Args != null)
                                    {
                                        arguments.AddRange(options.Args);
                                    }

                                    command.Execute([.. arguments]);
                                    command.ExecuteAsync([.. arguments]).Wait();
                                    return;
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error: {ex.Message}");
                                return;
                            }
                        });
                    return;
                }
            }

            host.Run();
        }
    }
}