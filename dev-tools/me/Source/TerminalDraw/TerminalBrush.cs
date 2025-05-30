namespace Me;

internal sealed class TerminalBrush : IBrush
{
    public ConsoleColor InitialColor { get => _defaultColor; }
    public ConsoleColor Color { get => _currentColor; set => _currentColor = value; }
    public void ResetColor() => _currentColor = _defaultColor;

    private ConsoleColor _defaultColor;
    private ConsoleColor _currentColor;

    public TerminalBrush(ConsoleColor defaultTextColor)
    {
        _defaultColor = defaultTextColor;
        _currentColor = defaultTextColor;
    }

}
