namespace Me;

internal enum StageResultEnum 
{
    Success,
    Failed,
    WithWarn
}

internal interface IStage<TElement>
    where TElement : IPipelineConsumable
{
    TElement ProcessingElement { get; }
    string Name { get; }
    string Errors { get; }
    string Warns { get; }
    StageResultEnum Proceed();
}
