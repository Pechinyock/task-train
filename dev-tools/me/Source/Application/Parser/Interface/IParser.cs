namespace Me;

internal interface IParser<TOut>
{
    TOut Parse(string input);
    TOut Parse(string[] inputs);
}
