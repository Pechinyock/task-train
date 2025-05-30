namespace Me;

internal sealed class Help : CommandBase
{
    public override CommandTypesEnumFlag Flags => CommandTypesEnumFlag.SubCommanded;

    public override string Alias => "help";

    public override void Execute()
    {
        throw new NotImplementedException();
    }
}
