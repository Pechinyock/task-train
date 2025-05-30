﻿namespace Me.Tests;

internal class TestCommand : CommandBase
{
    public override string Alias => "test";

    public override CommandTypesEnumFlag Flags => CommandTypesEnumFlag.Argumented
        | CommandTypesEnumFlag.SubCommanded
        | CommandTypesEnumFlag.Parzmetrized;

    public override void Execute()
    {

    }
}

internal class TestCommandLibrary : ICommandsStorage<CommandBase>
{

    private readonly Dictionary<string, CommandBase> _lib = new()
    {
        {"test", new TestCommand()}
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
    [InlineData("test -other", new string[] { "-other" })]
    [InlineData("test sub -other", new string[] { "-other" })]
    public void ParseCommandAndArgunets_Test(string input, string[] argsShoudBe)
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
}
