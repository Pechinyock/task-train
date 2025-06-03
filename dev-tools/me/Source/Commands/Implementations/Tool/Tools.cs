
namespace Me;

internal sealed class Tools : CommandBase
                            , ISubcommanded
{
    public override string Alias => "tools";
    public Dictionary<string, string> AvailableSubCommands => new()
    { 
        { "list", "List all external tolls that should be installed" }
    };

    public override string Description => "Handles everything about external tools. For example 'docker', 'dotnet', 'get' etc...";


    public override void Execute()
    {
        Print.Info("Tools");
    }
}
