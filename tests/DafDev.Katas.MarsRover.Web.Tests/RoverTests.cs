using System.Drawing;

namespace DafDev.Katas.MarsRover.Web;

public class RoverTests
{
    private readonly Rover _target;

    public RoverTests()
    {
        _target = new Rover();
    }

    [Fact]
    public void InitFuntion_ReturnsDefaultStartingPointAndDirection()
    {
        var result = _target.Init();
        Assert.Equal(0, result.Position.X);
        Assert.Equal(0, result.Position.Y);
        Assert.Equal('N', result.Direction);
    }

    [Fact]
    public void ReceivesCommands()
    {
        var commands = Array.Empty<char>();
        _target.GetCommands(commands);
    }

    [Theory]
    [MemberData(nameof(GetCommandsData))]
    public void ForwardCommandMovesRoverForwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        var rover = _target.Init();
        rover.Direction = direction;
        var commands = new[] { 'f' };

        rover.GetCommands(commands);

        Assert.Equal(direction, rover.Direction);
        Assert.Equal(expectedX, rover.Position.X);
        Assert.Equal(expectedY, rover.Position.Y);

    }

    [Theory]
    [InlineData('N',0,-1)]
    public void BackawarCommandMovesRoverBackwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        var rover = _target.Init();
        rover.Direction = direction;
        var commands = new[] { 'b' };

        rover.GetCommands(commands);

        Assert.Equal(direction, rover.Direction);
        Assert.Equal(expectedX, rover.Position.X);
        Assert.Equal(expectedY, rover.Position.Y);

    }



    public static IEnumerable<object[]> GetCommandsData()
    {
        yield return new object[] { 'N', 0, 1 };
        yield return new object[] { 'E', 1, 0 };
        yield return new object[] { 'S', 0, -1 };
        yield return new object[] { 'W', -1, 0 };
    }
}