namespace Me;

internal interface ICommandProjection
{
    bool IsSubcommandRequred { get; }
    Dictionary<string, string> AvailableSubCommands { get; }
    Dictionary<string, bool> AvailableParameters { get; }

}
