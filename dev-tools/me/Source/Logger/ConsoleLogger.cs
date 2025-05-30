namespace Me;

internal sealed class ConsoleLogger : ILogger
{
    private LogChannelsEnum _verbosity;

    public void SetVerbosity(LogChannelsEnum value) => _verbosity |= value;

    public void Trace(string msg)
    {
        if (!IsAllowedToBeLogged(msg, LogChannelsEnum.Trace))
            return;
        PrintColored(msg, ConsoleColor.Gray);
    }

    public void Info(string msg)
    {
        if (!IsAllowedToBeLogged(msg, LogChannelsEnum.Info))
            return;
        PrintColored(msg, ConsoleColor.Green);
    }

    public void Warn(string msg)
    {
        if (!IsAllowedToBeLogged(msg, LogChannelsEnum.Warn))
            return;
        PrintColored(msg, ConsoleColor.Yellow);
    }

    public void Error(string msg)
    {
        if (!IsAllowedToBeLogged(msg, LogChannelsEnum.Error))
            return;
        PrintColored(msg, ConsoleColor.Red);
    }

    private static void PrintColored(string msg, ConsoleColor color) => Print.PrintColored(msg, color);

    private bool IsAllowedToBeLogged(string msg, LogChannelsEnum level)
    {
        if (!_verbosity.HasFlag(level))
            return false;
        if (String.IsNullOrEmpty(msg))
            return false;

        return true;
    }
}
