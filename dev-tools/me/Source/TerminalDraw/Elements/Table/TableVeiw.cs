using System.Diagnostics;

namespace Me;

internal sealed class TableVeiw : IDrawable
{

    private readonly string[] _columns;
    private readonly Row[] _rows;

    private char _headerStroke = '=';
    private char _headerColumnSeporator = '|';

    private int[] _columnsWidthPrecents;
    private char _columnSeporator = '|';
    private char _rowSeporator = '-';
    private char _crossColumnRowSeporator = '+';
    private TextAlignmentEnum _titleAlignment = TextAlignmentEnum.Center;

    private IBrush _tableBrush;

    public TableVeiw(string[] columns, Row[] rows, IBrush brush)
    {
        Debug.Assert(columns is not null && columns.Length > 0, $"{nameof(columns)} is required and can't be null or empty");
        Debug.Assert(rows is not null && rows.Length > 0, $"{nameof(columns)} is required and can't be null or empty");
        Debug.Assert(brush is not null, "brush can't be null");

        _columns = columns;
        _rows = rows;
        _tableBrush = brush;
    }

    public void Draw(IBrush brush)
    {
        var headerRow = new Row([new Cell("column Title")
            , new Cell("column Title")]
        );
        DrawTableLine(headerRow);
    }

    private void DrawTableLine(Row row)
    {

    }
}
