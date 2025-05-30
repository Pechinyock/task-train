namespace Me;

internal interface IBrush
{
    ConsoleColor InitialColor { get; }
    ConsoleColor Color { get; set; }
    void ResetColor();
}
