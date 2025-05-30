namespace Me;

internal sealed class TerminalTextLine : IDrawable
{
    private readonly string _text;
    public TerminalTextLine(string text)
    {
        _text = text;
    }

    public void Draw(IBrush brush)
    {
        Console.ForegroundColor = brush.Color;
        Console.WriteLine(_text);
        Console.ForegroundColor = brush.InitialColor;
    }
}
