using System.Drawing;

namespace DafDev.Katas.MarsRover.Web;

public class RoverTests
{
    private readonly Rover _target;

    public RoverTests()
    {
        _target= new Rover();
    }

    [Fact]
    public void InitFuntion_ReturnsDefaultStartingPointAndDirection()
    {
        var result = _target.Init();
        Assert.Equal(0, result.StartingPoint.X);
        Assert.Equal(0, result.StartingPoint.Y);
        Assert.Equal('N', result.Direction);
    }

    [Fact]
    public void ReceivesCommands()
    {
        var commands = Array.Empty<char>();
        _target.GetCommands(commands);
    }

    [Fact]
    public void ForwardCommandMovesRoverForwardBy1UnitWithNorthStartingPosition()
    {
        var rover = _target.Init();
        
        var commands= new[] {'f'};

        rover.GetCommands(commands);

        Assert.Equal('N', rover.Direction);
        Assert.Equal(0, rover.StartingPoint.X);
        Assert.Equal(1, rover.StartingPoint.Y);

    }

    [Fact]
    public void ForwardCommandMovesRoverForwardBy1UnitWithEastStartingPosition()
    {
        var rover = _target.Init();
        rover.Direction = 'E';
        var commands = new[] { 'f' };

        rover.GetCommands(commands);

        Assert.Equal('E', rover.Direction);
        Assert.Equal(1, rover.StartingPoint.X);
        Assert.Equal(0, rover.StartingPoint.Y);

    }
}