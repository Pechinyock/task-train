namespace Me;

internal abstract class CommandBase
{
    public string SubCommand { get; set; }
    public string[] Arguments { get; set; }
    public Dictionary<string, string> Parameters { get; set; }

    public Dictionary<string, string> CommonArguments => new()
    {
        { "usage", "prints usage of command" }
    };

    public abstract string Description { get; }
    public abstract string Alias { get; }
    public abstract string Usage { get; }
    public abstract ICommandProjection Projection { get; }

    public abstract void Execute();
}
