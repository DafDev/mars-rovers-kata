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
}