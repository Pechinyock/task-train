namespace Me;

internal sealed class Row
{
    public bool IsCellEmpty(int index) => _cells[index] == null;
    public int CellsCount => _cells.Length;


    private readonly Cell[] _cells;

    public Row(params Cell[] cells)
    {
        _cells = cells;
    }
}
