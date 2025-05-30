using System.Text;

namespace Me;

internal sealed class Validate : IStage<CommandBase>
{
    public CommandBase ProcessingElement => _processingCmd;

    public string Name => "Parse";
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
        Log.Info($"Parse");
        return StageResultEnum.Success;
    }
}
