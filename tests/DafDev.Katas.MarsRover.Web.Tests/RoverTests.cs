using DafDev.Katas.MarsRover.Web.Navigation;

namespace DafDev.Katas.MarsRover.Web;

public class RoverTests
{
    private readonly Rover _target;
    private readonly Mock<IDriver> _driverMock = new();

    public RoverTests()
    {
        _target = new Rover(_driverMock.Object);
    }

    [Fact]
    public void RoverAtInstanciation_ReturnsDefaultStartingPointAndDirection()
    {
        //Arrange & Act
        var result = new Rover(_driverMock.Object);

        //Assert
        Assert.Equal(0, result.Position.X);
        Assert.Equal(0, result.Position.Y);
        Assert.Equal('N', result.Direction);
    }

    [Fact]
    public void ReceivesCommands()
    {
        //Arrange
        var commands = Array.Empty<char>();
        //Act
        _target.GetCommands(commands);
    }

    [Theory]
    [MemberData(nameof(GetForwardCommandData))]
    public void ForwardCommandMovesRoverForwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        //Arrange
        var rover = new Rover(_driverMock.Object)
        {
            Direction = direction
        };
        var commands = new[] { 'f' };

        //Act
        rover.GetCommands(commands);

        //Assert
        Assert.Equal(direction, rover.Direction);
        Assert.Equal(expectedX, rover.Position.X);
        Assert.Equal(expectedY, rover.Position.Y);

    }

    [Theory]
    [MemberData(nameof(GetBackwardCommandData))]
    public void BackwardCommandMovesRoverBackwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        //Arrange
        _driverMock
            .Setup(d => d.MoveBackward(It.IsAny<Coordinates>(), It.IsAny<char>()))
            .Returns(new Coordinates(expectedX, expectedY));
        var rover = new Rover(_driverMock.Object)
        {
            Direction = direction
        };
        var commands = new[] { 'b' };

        //Act
        rover.GetCommands(commands);

        //Assert
        Assert.Equal(direction, rover.Direction);
        Assert.Equal(expectedX, rover.Position.X);
        Assert.Equal(expectedY, rover.Position.Y);

    }


    public static IEnumerable<object[]> GetForwardCommandData()
    {
        yield return new object[] { 'N', 0, 1 };
        yield return new object[] { 'E', 1, 0 };
        yield return new object[] { 'S', 0, -1 };
        yield return new object[] { 'W', -1, 0 };
    }

    public static IEnumerable<object[]> GetBackwardCommandData()
    {
        yield return new object[] { 'N', 0, -1 };
        yield return new object[] { 'E', -1, 0 };
        yield return new object[] { 'S', 0, 1 };
        yield return new object[] { 'W', 1, 0 };
    }
}