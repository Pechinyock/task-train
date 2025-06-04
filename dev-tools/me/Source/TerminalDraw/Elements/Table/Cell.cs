namespace Me;

internal sealed class Cell 
{
    public string Data => _data;
    public ushort WidthPrecents => _widthPrecents;
    public TextAlignmentEnum TextAlignmentEnum => _textAlignment;

    private readonly string _data;
    private readonly ushort _widthPrecents;
    private readonly TextAlignmentEnum _textAlignment;

    public Cell(string data
        , ushort widthPrecents = 0
        , TextAlignmentEnum textAlignment = TextAlignmentEnum.Left)
    {
        _data = data;
        _textAlignment = textAlignment;
        _widthPrecents = widthPrecents;
    }
}
