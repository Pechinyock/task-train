namespace Me;

internal sealed class CommandsLibrary : ICommandsStorage<CommandBase>
{
    private static readonly Dictionary<char, Dictionary<string, CommandBase>> LibraryMap = new()
    {
        { 'h', H.Get() },
    };


    public CommandBase Request(string alias)
    {
        if(String.IsNullOrWhiteSpace(alias))
            return null;

        var toLower = alias.ToLower();
        var firstChar = toLower[0];

        if(!LibraryMap.ContainsKey(firstChar))
            return null;

        var shelf = LibraryMap[firstChar];

        if(!shelf.ContainsKey(toLower))
            return null;

        return shelf[toLower];
    }

    public CommandBase[] Request(char letter)
    {
        if(LibraryMap.ContainsKey(letter))
            return null;

        return LibraryMap[letter].Values.ToArray();
    }

    public CommandBase[] RequestAll()
    {
        int totalElement = 0;
        foreach (var shelf in LibraryMap)
        {
            totalElement += shelf.Value.Count;
        }
        var result = new CommandBase[totalElement];
        for (int i = 0; i < totalElement;)
        {
            foreach (var shelf in LibraryMap)
            {
                foreach (var command in shelf.Value)
                {
                    result[i] = command.Value;
                    ++i;
                }
            }
        }
        return result;
    }
}
