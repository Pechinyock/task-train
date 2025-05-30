using System.Diagnostics;

namespace Me;

internal abstract class PipelineBase<TElement>
    where TElement : IPipelineConsumable
{
    protected abstract IStage<TElement>[] Stages { get; }

    public void Start() 
    {
        foreach (var (index, stage) in Stages.Index()) 
        {
            try
            {
                var result = stage.Proceed();
                var element = stage.ProcessingElement;
                if (result == StageResultEnum.Success)
                {
                    Log.Trace($"Stage: {stage.Name} witn command: {element?.Alias} is done successufly!");
                }
                else if (result == StageResultEnum.WithWarn)
                {
                    Log.Warn($"Stage: {stage.Name} has been done with warns:");

                    var warns = stage.Warns;
                    Debug.Assert(!String.IsNullOrWhiteSpace(warns), "warns has to be provided!");
                    Log.Warn(warns);
                }
                else if (result == StageResultEnum.Failed)
                {
                    Log.Error($"Pipeline breaks at stage number: {index} \n" +
                        $" stage name: {stage.Name}\n" +
                        $" command name: {element?.Alias}"
                    );

                    var errors = stage.Errors;
                    Debug.Assert(!String.IsNullOrWhiteSpace(errors), 
                        "Drops with errors, but error(s) description(s) has not been provided"
                    );

                    Log.Error(errors);
                    break;
                }
                else
                    Debug.Assert(false, "Unknow stage type result!");
            }
            catch (Exception ex) 
            {
                /** Fatal error! */
                Log.Error($"{ex.Message}");
                Debug.Assert(false, "This should never happen!");
                break;
            }
        }
    }
}
