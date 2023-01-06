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
        Assert.Equal(0, result.X);
        Assert.Equal(0, result.Y);
        Assert.Equal('N', result.Direction);
    }
}