using System.Diagnostics;

namespace Me;

internal sealed class TablePattern
{
    public int ColumnCount => _columnCount;

    private readonly int _columnCount;
    private readonly char _headerStroke;

    public TablePattern(in int cloumnCount, in char headerStroke = '-')
    {
        Debug.Assert(cloumnCount > 0);

        _columnCount = cloumnCount;
        _headerStroke = headerStroke;
    }

    /** Hide on perpuse. There's several required parameters for this class */
    private TablePattern() { }
}