namespace Me;

internal sealed class Row 
{
    private readonly Cell[] _cells;

    public Cell this[int index] => _cells[index];

    public Row(Cell[] cells, TextAlignmentEnum textAlignment = TextAlignmentEnum.Left)
    { 
        _cells = cells;
    }
}
