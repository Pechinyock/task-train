namespace Me;

internal sealed class CommandParser : IParser<CommandBase>
{
    private readonly ICommandsStorage<CommandBase> _storage;

    private const string ARGUMENT_INDICATOR = "-";

    public CommandParser(ICommandsStorage<CommandBase> storage)
    {
        _storage = storage;
    }

    public CommandBase Parse(string input)
    {
        if (String.IsNullOrWhiteSpace(input))
            return null;

        var tokens = input.Split(' ');
        var cmd = Parse(tokens);

        return cmd;
    }

    public CommandBase Parse(string[] input) 
    {
        var alias = input[0];
        var founded = _storage.Request(alias);

        if (founded is null) 
        {
            Print.Error($"Couldn't find command with alias: {alias}");
            return null;
        }

        var commandInfo = input.Skip(1)
            .ToArray();

        if (commandInfo.Length == 0)
            return founded;

        ExtractData(commandInfo, founded);

        return founded;
    }

    private static void ExtractData(string[] input, CommandBase writeHere)
    {
        if (!input[0].StartsWith(ARGUMENT_INDICATOR)) 
            writeHere.SubCommand = input[0];

        var args = new List<string>();
        var parameters = new Dictionary<string, string>();
        for (int i = 0; i < input.Length; ++i) 
        {
            var arg = input[i];
            if (!arg.StartsWith(ARGUMENT_INDICATOR))
                continue;

            var lastElementIndex = input.Length - 1;
            if (i == lastElementIndex)
            {
                args.Add(arg);
                break;
            }

            var nextElementIndex = i + 1;
            if (input[nextElementIndex].StartsWith(ARGUMENT_INDICATOR))
            {
                args.Add(arg);
            }
            else
            {
                /*[STOPS HERE]
                 * Cut indicator
                 * test results
                 */
                var parameterAlias = input[i];//.Skip(ARGUMENT_INDICATOR.Length).ToString();
                parameters.Add(parameterAlias, input[nextElementIndex]);
            }
        }
        writeHere.Arguments = args.Count != 0
            ? args.ToArray()
            : null;

        writeHere.Parameters = parameters.Count != 0
            ? parameters
            : null;
    }
}
