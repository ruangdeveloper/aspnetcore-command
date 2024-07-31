using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RuangDeveloper.AspNetCore.Command
{
    /// <summary>
    /// Represents the options for the command.
    /// </summary>
    public class Options
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        [Option("command", Required = true, HelpText = "Command to execute")]
        public string? Command { get; set; }

        /// <summary>
        /// Gets or sets the arguments for the command.
        /// </summary>
        [Option("args", Required = false, HelpText = "Arguments for the command")]
        public IEnumerable<string>? Args { get; set; }
    }

    /// <summary>
    /// Command runner.
    /// </summary>
    public static class CommandRunner
    {
        /// <summary>
        /// Runs the host with the commands.
        /// </summary>
        /// <param name="host"></param>
        /// <param name="args"></param>
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