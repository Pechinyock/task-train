using System.Text;

namespace Me;

internal sealed class TerminalTextLine : IDrawable
{
    private StringBuilder _text;
    private readonly TextAlignmentEnum _textAlignment;

    public TerminalTextLine(string text, TextAlignmentEnum textAlignment = TextAlignmentEnum.Left)
    {
        _text = new StringBuilder(text);
        _textAlignment = textAlignment;
    }

    public void Draw(IBrush brush)
    {
        Console.ForegroundColor = brush.Color;
        DrawText();
        Console.ForegroundColor = brush.InitialColor;
    }

    private void DrawText() 
    {
        switch (_textAlignment)
        {
            case TextAlignmentEnum.Center:
                break;
            case TextAlignmentEnum.Right:
                break;
        }
        Console.WriteLine(_text);
    }
}
