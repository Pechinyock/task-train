﻿namespace Me;

internal sealed class CommandParser : IParser<CommandBase>
{
    private readonly IStorage<CommandBase> _storage;

    private const string COMMAND_INPUT_INDICATOR = "-";
    private const char COMMAND_PARAMETER_KEY_VALUE_SEPORATOR = ':';

    public CommandParser(IStorage<CommandBase> storage)
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

    /** [Input format]
     * {command-alias} {sub-command}? {-switch}? {-parameterName:parameterValue}?
     */
    public CommandBase Parse(string[] input)
    {
        var alias = input[0];
        var founded = _storage.Request(alias);

        if (founded is null)
        {
            Print.Error($"Command not found: {alias}");
            return null;
        }

        var commandInfo = input.Skip(1)
            .ToArray();

        if (commandInfo.Length == 0)
            return founded;

        if (!commandInfo[0].StartsWith(COMMAND_INPUT_INDICATOR))
        {
            founded.SubCommand = commandInfo[0];
        }

        var foundedArgs = new List<string>(5);
        var unexpected = new List<string>();

        for (int i = 0; i < commandInfo.Length; ++i)
        {
            if (!commandInfo[i].StartsWith(COMMAND_INPUT_INDICATOR))
                continue;

            bool wasParameter = false;

            foreach (var (index, letter) in commandInfo[i].Index())
            {
                if (letter != COMMAND_PARAMETER_KEY_VALUE_SEPORATOR)
                    continue;

                var cmdParameter = commandInfo[i];

                var startFromActualParamName = 1;
                var endBeforeSeporatorKVP = index - 1;
                var key = cmdParameter.Substring(startFromActualParamName, endBeforeSeporatorKVP);

                var indexNextToParamIndicator = index + 1;
                var cutFromIndicatorToEnd = cmdParameter.Length - index - 1;
                var value = cmdParameter.Substring(indexNextToParamIndicator, cutFromIndicatorToEnd);

                if (founded.Parameters is null)
                    founded.Parameters = new();

                founded.Parameters.Add(key, value);
                wasParameter = true;
                break;
            }

            if (wasParameter)
                continue;

            var argWithoutIndicator = commandInfo[i].Substring(COMMAND_INPUT_INDICATOR.Length);

            foundedArgs.Add(argWithoutIndicator);
        }

        var argsResult = foundedArgs
            .Where(x => x is not null)
            .ToArray();

        founded.Arguments = argsResult.Length == 0
            ? null
            : argsResult;

        return founded;
    }
}
