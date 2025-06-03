namespace Me;

internal interface ISubcommanded
{
    Dictionary<string, string> AvailableSubCommands { get; }
}