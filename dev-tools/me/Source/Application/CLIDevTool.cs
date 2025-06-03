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

        if (input is null || input.Length == 0)
        {
            Print.Error($"input couldn't be empty");
            return;
        }

        var commandStorage = new CommandsLibrary();
        var parser = new CommandParser(commandStorage);

        var cmd = parser.Parse(input);
        if (cmd is null)
        {
            Print.Error($"Couldn't find command : {input[0]}");
            return;
        }

        var pipeline = new CommandsPipeline(cmd);

        pipeline.Start();
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
