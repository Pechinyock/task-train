using System.Diagnostics;

namespace Me;

internal sealed class Validate : IStage<CommandBase>
{
    public CommandBase ProcessingElement => _processingCmd;

    public string Name => "Validate";
    public string Errors => errors;
    public string Warns => warns;

    private string errors;
    private string warns;
    private readonly CommandBase _processingCmd;

    public Validate(CommandBase command)
    {
        _processingCmd = command;
    }

    public StageResultEnum Proceed()
    {
        if (!String.IsNullOrWhiteSpace(_processingCmd.SubCommand))
        {
            var subcommanded = _processingCmd as ISubcommanded;
            Debug.Assert(subcommanded is not null);

            var allSubCommands = subcommanded.AvailableSubCommands;
            if (!allSubCommands.ContainsKey(_processingCmd.SubCommand))
            {
                errors = $"Unknown subcommand: {_processingCmd.SubCommand}\n";
                return StageResultEnum.Failed;
            }
        }
        return StageResultEnum.Success;
    }
}
