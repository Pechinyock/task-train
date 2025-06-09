namespace Me;

internal static class Print
{
    private static readonly IBrush _defautlBrush = new TerminalBrush(ConsoleColor.White);

    public static void PrintColored(string msg, ConsoleColor color)
    {
        var text = new TerminalText(msg);
        _defautlBrush.Color = color;
        text.Draw(_defautlBrush);
    }

    public static void String(string message) => PrintColored(message, ConsoleColor.White);
    public static void Error(string messgage) => PrintColored(messgage, ConsoleColor.Red);
    public static void Info(string message) => PrintColored(message, ConsoleColor.Green);
    public static void Warn(string message) => PrintColored(message, ConsoleColor.Yellow);

    public static void DrawElement(IDrawable element, IBrush brush) => element.Draw(brush);
}
