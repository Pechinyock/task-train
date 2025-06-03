

namespace Me;

internal sealed class Help : CommandBase
                           , ISubcommanded
{
    public override string Alias => "help";

    public override string Description => "Use it! It will pretend that is is helpful...";

    public Dictionary<string, string> AvailableSubCommands => GetAvailableSubCommnds();

    public override void Execute()
    {
        foreach (var command in GetAllCommands())
        {
            Print.Info($"{command.Alias} : {command.Description}");
        }
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

    private static Dictionary<string, string> GetAvailableSubCommnds() 
    {
        var commands = GetAllCommands()
            .Select(x => x.Alias)
            .ToDictionary(key=>key);
        return commands;
    }
}
