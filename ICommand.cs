namespace RuangDeveloper.AspNetCore.Command;

public interface ICommand
{
    string Name { get; }
    string Description { get; }
    Task ExecuteAsync(string[] args);
    void Execute(string[] args);
}