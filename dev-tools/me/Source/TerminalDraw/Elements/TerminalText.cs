using System.Text;
using System.Diagnostics;

namespace Me;

internal sealed class TerminalText : IDrawable
{
    public const char NEW_LINE_CHARACTER = '\n';

    private StringBuilder _text;
    private readonly TextAlignmentEnum _textAlignment;

    public TerminalText(string text, TextAlignmentEnum textAlignment = TextAlignmentEnum.Left)
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
        if(_textAlignment != TextAlignmentEnum.Left)
            Format();

        Console.WriteLine(_text);
    }

    /* [TODO]
     * If line breaks because of overflow need to
     * break it at the end of the word 
     */
    private void Format()
    {
        var characterCount = _text.Length;
        const int offsetForAlignment = 2;
        var consoleWidth = Console.WindowWidth - offsetForAlignment;

        var formatedText = new StringBuilder();

        bool fitInSingleLine = _text.Length <= consoleWidth;

        if (fitInSingleLine) 
        {
            var leftSpacesCount = _textAlignment == TextAlignmentEnum.Center
                ? (consoleWidth - _text.Length) / 2
                : (consoleWidth - _text.Length);

            if (leftSpacesCount > 0)
                formatedText.Append(' ', leftSpacesCount);

            formatedText.Append(_text);
            _text = formatedText;
            return;
        }

        for (int textIterator = 0, lineSymolsCount = 0, lastAdditionCursorIndex = 0;
            textIterator < _text.Length;
            ++textIterator)
        {
            bool isOverflowed = lineSymolsCount == consoleWidth;
            bool isNewLineCharacter = _text[textIterator] == NEW_LINE_CHARACTER;
            bool isEndOfWholeText = textIterator == _text.Length - 1;

            ++lineSymolsCount;

            if (isNewLineCharacter || isOverflowed || isEndOfWholeText)
            {
                var leftSpacesCount = _textAlignment == TextAlignmentEnum.Center
                    ? (consoleWidth - lineSymolsCount) / 2
                    : (consoleWidth - lineSymolsCount);

                if(leftSpacesCount > 0)
                    formatedText.Append(' ', leftSpacesCount);

                var appendUnless = Char.IsLetterOrDigit(_text[textIterator])
                        ? FindLeftClosesSpaceIndex(_text, textIterator)
                        : textIterator;

                for (; lastAdditionCursorIndex <= appendUnless; ++lastAdditionCursorIndex)
                {
                    formatedText.Append(_text[lastAdditionCursorIndex]);
                }

                if (isOverflowed)
                    formatedText.Append(NEW_LINE_CHARACTER);

                lineSymolsCount = 0;
            }
        }

        _text = formatedText;
    }

    public static int FindLeftClosesSpaceIndex(StringBuilder sb, int startAt)
    {
        for (int i = startAt; ; --i)
        {
            if (sb[i] == ' ')
                return i;
            if (i <= 0)
                Debug.Assert(false, "what the fuck?");
        }
    }
}
