namespace Me;

internal sealed class H
{
    public static Dictionary<string, CommandBase> Get() => Shelf;

    private static readonly Dictionary<string, CommandBase> Shelf = new Dictionary<string, CommandBase>() 
    {
        { "help", new Help() }
    };
}
