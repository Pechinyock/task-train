namespace Me.Tests;

internal class TestCommand : CommandBase
{
    public override string Alias => "test";

    public override void Execute()
    {

    }
}

internal class TestCommandLibrary : IStorage<CommandBase>
{

    private readonly Dictionary<string, CommandBase> _lib = new()
    {
        { "test", new TestCommand() }
    };

    public CommandBase Request(string alias)
    {
        if (!_lib.ContainsKey(alias))
            return null;

        return _lib[alias];
    }

    public CommandBase[] Request(char letter)
    {
        throw new NotImplementedException();
    }

    public CommandBase[] RequestAll()
    {
        throw new NotImplementedException();
    }
}

public class CommandParserTests
{
    [Fact]
    public void InstanceCreate_Sucess()
    {
        var errors = Record.Exception(() =>
        {
            var storage = new CommandsLibrary();
            var parser = new CommandParser(storage);
        });

        Assert.Null(errors);
    }

    [Theory]
    [InlineData("test", true)]
    [InlineData("sfdsf", false)]
    public void ParseCommandOnly_Test(string input, bool parsed)
    {
        var storage = new TestCommandLibrary();
        var parser = new CommandParser(storage);
        var result = parser.Parse(input);

        if (parsed)
            Assert.NotNull(result);
        else
            Assert.Null(result);
    }

    [Theory]
    [InlineData("test", null)]
    [InlineData("test sub", "sub")]
    [InlineData("test subCmd other shit", "subCmd")]
    [InlineData("test create -other -shit", "create")]
    public void ParseCommandAndSubCommand_Test(string input, string subCommandShouldBe)
    {
        var storage = new TestCommandLibrary();
        var parser = new CommandParser(storage);
        var result = parser.Parse(input);

        Assert.NotNull(result);
        Assert.Equal(subCommandShouldBe, result.SubCommand);
    }

    [Theory]
    [InlineData("test", null)]
    [InlineData("test sub", null)]
    [InlineData("test -other", new string[] { "other" })]
    [InlineData("test sub -other", new string[] { "other" })]
    public void ParseCommandAndArgumets_Test(string input, string[] argsShoudBe)
    {
        var storage = new TestCommandLibrary();
        var parser = new CommandParser(storage);
        var result = parser.Parse(input);

        if (argsShoudBe is null)
        {
            Assert.Null(result.Arguments);
            return;
        }

        Assert.NotNull(result);
        foreach (var (index, argument) in result.Arguments.Index())
        {
            Assert.Equal(argsShoudBe[index], argument);
        }
    }

    [Theory]
    [InlineData("test", null, null)]
    [InlineData("test sub", null, null)]
    [InlineData("test -param:value"
        , new string[] { "param" }
        , new string[] { "value" }
    )]
    [InlineData("test sum -param:value"
        , new string[] { "param" }
        , new string[] { "value" }
    )]
    [InlineData("test sum -param:value -name:hui -drunkArg -withLog:remote"
        , new string[] { "param", "name", "withLog" }
        , new string[] { "value", "hui", "remote" }
    )]
    [InlineData("test sum -param:value -name:hui justRundomText -drunkArg -withLog:remote"
        , new string[] { "param", "name", "withLog" }
        , new string[] { "value", "hui", "remote" }
    )]
    public void ParseCommandAndParameters_Test(string input, string[] expectingAddedKeys, string[] expectingAddedValues)
    {
        var storage = new TestCommandLibrary();
        var parser = new CommandParser(storage);
        var result = parser.Parse(input);

        if (expectingAddedKeys is null)
        {
            Assert.Null(result.Parameters);
            return;
        }

        Assert.NotNull(result);
        foreach (var parameter in expectingAddedKeys)
        {
            Assert.True(result.Parameters.ContainsKey(parameter));
        }

        for (int i = 0; i < expectingAddedValues.Length; ++i)
        {
            var addedValue = result.Parameters[expectingAddedKeys[i]];
            var expectedValue = expectingAddedValues[i];
            Assert.Equal(expectedValue, addedValue);
        }
    }
}
