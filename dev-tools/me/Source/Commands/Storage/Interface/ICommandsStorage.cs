namespace Me;

internal interface ICommandsStorage<TType>
{
    TType Request(string alias);
    TType[] Request(char letter);
    TType[] RequestAll();
}
