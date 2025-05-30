namespace Me;

internal static class CommandExtensions
{
    public static bool IsParametrized(this CommandBase command)
    {
        return command.Flags.HasFlag(CommandTypesEnumFlag.Parzmetrized);
    }

    public static bool IsSubCommanded(this CommandBase command)
    {
        return command.Flags.HasFlag(CommandTypesEnumFlag.SubCommanded);
    }

    public static bool IsArgumented(this CommandBase command)
    {
        return command.Flags.HasFlag(CommandTypesEnumFlag.Argumented);
    }
}

internal enum CommandTypesEnumFlag
{
    None            = 0,
    Parzmetrized    = 1 << 0,
    Argumented      = 1 << 1,
    SubCommanded    = 1 << 2,
}

internal abstract class CommandBase : IPipelineConsumable
{
    public abstract string Alias { get; }
    public string SubCommand { get; set; }
    public string[] Arguments { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
    public abstract CommandTypesEnumFlag Flags { get; }

    public abstract void Execute();
}
