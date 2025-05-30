namespace Me;

internal static class EntryPoint
{
    internal static void Main(string[] args) 
    {
        var app = new CLIDevTool();
        app.Run(args);
    }
}
