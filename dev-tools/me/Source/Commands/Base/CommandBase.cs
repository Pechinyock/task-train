namespace Me;

internal abstract class CommandBase : IPipelineConsumable
{
    public abstract string Alias { get; }
    public string SubCommand { get; set; }
    public string[] Arguments { get; set; }
    public Dictionary<string, string> Parameters { get; set; }

    public abstract string Description { get; }

    public abstract void Execute();
}
