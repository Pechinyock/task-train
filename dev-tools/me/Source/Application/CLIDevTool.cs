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
        var input = Args(args)
            .Where(x => !String.IsNullOrWhiteSpace(x))
            .ToArray();

        var commandStorage = new CommandsLibrary();
        var parser = new CommandParser(commandStorage);

        var cmd = parser.Parse(input);
        if (cmd is null)
            Print.Error($"Couldn't find command : {input}");

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
        return userInput.Split(' ');
#endif
        return args;
    }
}
