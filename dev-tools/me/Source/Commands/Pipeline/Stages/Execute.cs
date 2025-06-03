namespace Me;

internal class Execute : IStage<CommandBase>
{
    public CommandBase ProcessingElement => _commandToExecute;
    public string Name => "Execute";
    public string Errors => _errors;
    public string Warns => _warns;

    private readonly CommandBase _commandToExecute;

    private string _errors;
    private string _warns;

    public Execute(CommandBase commandToExecute)
    {
        _commandToExecute = commandToExecute;
    }

    public StageResultEnum Proceed()
    {
        _commandToExecute.Execute();

        return StageResultEnum.Success;
    }
}
