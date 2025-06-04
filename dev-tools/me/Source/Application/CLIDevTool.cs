using System.Diagnostics;

namespace Me;

internal sealed class CLIDevTool
{
    public CLIDevTool()
    {
        var log = GetLogger();
        Log.InitializeLogger(log);
    }

    public void Run(string[] args)
    {
        var input = Args(args)?
            .Where(x => !String.IsNullOrWhiteSpace(x))
            .ToArray();

        var commandStorage = new CommandsLibrary();
        var commandParser = new CommandParser(commandStorage);

        var commandToExecute = commandParser.Parse(input);

        if (commandToExecute is null)
            return;

        if (!IsCommandValid(commandToExecute))
            return;

        commandToExecute.Execute();
    }

    private static bool IsCommandValid(CommandBase command) 
    {
        bool result = true;
        var subCommand = command.SubCommand;

        var projection = command.Projection;
        Debug.Assert(projection is not null, "Projection can't be null");

        var isSubCommandRequired = projection.IsSubcommandRequred;
        var isSubCommandFilled = !String.IsNullOrEmpty(subCommand);

        if (isSubCommandRequired && !isSubCommandFilled)
        {
            Print.Error($"{command.Alias} requires subcommand!");
            return false;
        }

        if (isSubCommandFilled)
        {
            var availableSubCommands = projection.AvailableSubCommands;
            Debug.Assert(availableSubCommands is not null || availableSubCommands.Count > 0
                , "Implements interface but doesn't have any subcommands"
            );

            if (!availableSubCommands.ContainsKey(command.SubCommand)) 
            {
                result = false;
                Print.Error($"The command: '{command.Alias}' doesn't have '{subCommand}' subcommand");
            }
        }

        return result;
    }

    private static ILogger GetLogger() 
    {
#if DEBUG
        var debugLogger = new ConsoleLogger();
        debugLogger.SetVerbosity(LogChannelsEnum.All);
        return debugLogger;
#endif
        var logger = new ConsoleLogger();
        logger.SetVerbosity(LogChannelsEnum.Error | LogChannelsEnum.Warn | LogChannelsEnum.Info);
        return logger;
    }

    private static string[] Args(string[] args) 
    {
#if DEBUG
        var userInput = Console.ReadLine();
        if (String.IsNullOrWhiteSpace(userInput)) 
            return null;

        return userInput.Split(' ');
#endif
        return args;
    }
}
