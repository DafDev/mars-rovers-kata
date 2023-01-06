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
    public void ReturnsStartingPointAndDirection()
    {
        var result = _target.Init();
        Assert.Equal(new Point(0,0), result.StartingPoint);
        Assert.Equal('N', result.Direction);
    }

    [Fact]
    public void ReceivesCommands()
    {
        var commands = Array.Empty<char>();
        _target.GetCommands(commands);
    }

    [Fact]
    public void ForwardCommandMovesRoverForwardBy1Unit()
    {
        var rover = _target.Init();
        
        var commands= new[] {'f'};

        rover.GetCommands(commands);

        Assert.Equal('N', rover.Direction);
        Assert.Equal(new Point(0, 1), rover.StartingPoint);

    }
}