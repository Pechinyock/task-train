namespace Me;

internal sealed class HelpProjection : ICommandProjection
{
    public bool IsSubcommandRequred => false;
    public Dictionary<string, string> AvailableSubCommands => Help.GetAvailableSubCommnds();
    public Dictionary<string, bool> AvailableParameters => null;
}

internal sealed class Help : CommandBase
{
    public override string Alias => "help";
    public override string Description => "Use it! It will pretend that is is helpful...";
    public override string Usage => "help\n help {any other command}\n help {character}\n"; 
    public override ICommandProjection Projection => new HelpProjection();

    public override void Execute()
    {
        var brush = new TerminalBrush(Console.ForegroundColor);
        brush.Color = ConsoleColor.DarkGreen;
        var text = new TerminalTextLine("this one is a large text that should be display in console as cool as possible but I don't sure if it is work actualy", TextAlignmentEnum.Center);
        text.Draw(brush);
    }

    internal static Dictionary<string, string> GetAvailableSubCommnds() 
    {
        var commands = GetAllCommands()
            .Select(x => x.Alias)
            .ToDictionary(key => key);

        var letters = new Dictionary<string, string>();

        for (char letter = 'a'; letter <= 'z'; ++letter) 
        {
            foreach (var cmd in commands) 
            {
                if(cmd.Key.StartsWith(letter))
                    letters.Add($"{letter}", null);
            }
        }

        var result = commands.Concat(letters)
            .ToDictionary(key => key.Key, value => value.Value);

        return result;
    }

    private static CommandBase[] GetAllCommands() 
    {
        var lib = new CommandsLibrary();

        Action printSubCommandInfo = () => {};

        var allCommands = lib.RequestAll()
            .Where(x => x.Alias != "help")
            .ToArray();

        return allCommands;
    }
}
