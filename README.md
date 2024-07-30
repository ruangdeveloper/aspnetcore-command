# ASP.NET Command

This package helps you add command features to your ASP.NET Core project, similar to Artisan in Laravel.

## Installation and Usage

### Install

```shell
dotnet add package RuangDeveloper.AspNetCore.Command
```

### Create Command

In your project, create a folder called Commands (you can use any name you like).

In the folder you created, add your command class. For example:

```csharp
using System;
using System.Threading.Tasks;
using RuangDeveloper.AspNetCore.Command;

public class HelloWorldCommand : ICommand
{
    public string Name => "hello";

    public async Task ExecuteAsync(string[] args)
    {
        // Simulate asynchronous work with Task.Delay
        await Task.Delay(1000);

        Console.WriteLine("Hello, world! (Async)");
    }

    // Synchronous execution of the command
    public void Execute(string[] args)
    {
        Console.WriteLine("Hello, world! (Sync)");
    }
}
```

Explanation:
- Name Property:
  The Name property returns the name of the command, which is `hello` in this example. This property allows you to identify the command when you’re executing it from a collection of commands.
- ExecuteAsync Method:
  The ExecuteAsync method performs the command asynchronously. Here, it’s simulating a delay using Task.Delay(1000) to mimic asynchronous operations, such as making a network call or accessing a database.
  After the delay, it prints “Hello, world! (Async)” to the console. If any arguments are passed, they are displayed as well.
- Execute Method:
  The Execute method performs the command synchronously. It simply prints “Hello, world! (Sync)” to the console.

### Register the Command

After your command class is created, you need to register it in the service collection.

```csharp
builder.Services.AddCommands(configure =>
{
    configure.AddCommand<HelloWorldCommand>();
});
```

Here, you use the `AddCommands` extension and then use the configuration to add the Command class.

### Modify How Your Project Runs

Typically, your project will run using the `.Run()` method, but to be able to execute Commands, you need to change it to `.RunWithCommands()`.

```csharp
// Change this
// app.Run();
// to this
app.RunWithCommands(args);
```

### Execute the Command

You can run the command using the following command:

```shell
dotnet run command --command hello
```

The `--command` option specifies which command to run, in this case, hello. This must match the name you set in your command class.

You can also add arguments using the `--args` option:

```shell
dotnet run command --command hello --args world
```

Then you can retrieve the arguments passed to your command.

```csharp
...
public void Execute(string[] args)
{
    Console.WriteLine($"Hello, {args.FirstOrDefault() ?? "world"}! (Sync)");
}
...
```

### (Optional) Modify Command Identifier

By default, you need to run the command with the following command:

```shell
dotnet run command --command hello
```

The keyword `command` is the identifier used to determine whether you want to execute a command or run the application.

If needed, you can change it to something like the following:

```shell
dotnet run cmd --command hello
```

To change it, you need to modify the configuration when registering the Command.

```csharp
builder.Services.AddCommands(configure =>
{
    // Change command identifier
    configure.SetCommandCallIdentifier("cmd");
    // Add command
    configure.AddCommand<HelloWorldCommand>();
});
```

### Built-in Commands
|Command|Description|Usage|
|-|-|-|
|`ls`|List all registered commands|`dotnet run command --command ls`|