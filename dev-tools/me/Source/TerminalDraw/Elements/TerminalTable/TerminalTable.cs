using System.Diagnostics;

namespace Me;

internal sealed class TerminalTable : IDrawable
{
    private readonly TablePattern _pattern;
    private List<Row> _rows;

    public TerminalTable(in TablePattern pattern)
    {
        _pattern = pattern;
    }

    public void Draw(IBrush brush)
    {
        var text = new TerminalText($"Table ready to draw table:\n Columns count: {_pattern.ColumnCount}");
        text.Draw(brush);
    }

    public void AddRow(params string[] data)
    {
        var cells = new Cell[data.Length];
        for (var i = 0; i < data.Length; ++i) 
        {
            cells[i] = new Cell(data[i]);
        }
        var row = new Row(cells);
        _rows.Add(row);
    }

    public void AddRows(params string[][] data) 
    {
        for (var i = 0; i < data.Length; ++i) 
        {
            var cells = new Cell[data.Length];
            for (var j = 0; j < data[i].Length; ++j) 
            {
                /*[Stops here] */

            }
        }
    }
}
