namespace Me;

internal sealed class T
{
    public static Dictionary<string, CommandBase> Get() => Shelf;

    private static readonly Dictionary<string, CommandBase> Shelf = new Dictionary<string, CommandBase>()
    {
        { "tools", new Tools() }
    };
}