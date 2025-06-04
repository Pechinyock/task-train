namespace Me;

internal sealed class ToolsProjection : ICommandProjection
{
    public bool IsSubcommandRequred => true;

    public Dictionary<string, string> AvailableSubCommands => new()
    {
        { "list", "List all external tools that should be installed" },
    };

    public Dictionary<string, bool> AvailableParameters => null;
}

internal sealed class Tools : CommandBase
{
    public override string Alias => "tools";
    public override string Description => "Handles everything about external tools. For example 'docker', 'dotnet', 'get' etc...";
    public override string Usage => "tools {subcommand}";
    public override ICommandProjection Projection => new ToolsProjection();

    public override void Execute()
    {
        Print.Info($"usage: {Usage}");
    }
}
