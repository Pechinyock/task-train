namespace Me;

internal interface IStorage<TType>
{
    TType Request(string alias);
    TType[] Request(char letter);
    TType[] RequestAll();
}
