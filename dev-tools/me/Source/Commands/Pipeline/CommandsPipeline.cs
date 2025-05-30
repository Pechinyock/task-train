namespace Me;

internal sealed class CommandsPipeline : PipelineBase<CommandBase>
{
    protected override IStage<CommandBase>[] Stages => _commandStages;

    private readonly IStage<CommandBase>[] _commandStages;

    public CommandsPipeline(CommandBase command)
    {
        _commandStages = [
            new Validate(command)
        ];
    }
}
