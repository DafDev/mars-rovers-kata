using DafDev.Katas.MarsRover.Web.Navigation;
using DafDev.Katas.MarsRover.Web.Tests.Data;

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
    [MemberData(nameof(RoverTestData.GetForwardCommandData), MemberType = typeof(RoverTestData))]
    public void ForwardCommandMovesRoverForwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        //Arrange
        var  startingPosition = new Coordinates(0,0);
        _driverMock
            .Setup(d => d.MoveForward(It.Is<Coordinates>(c => c.X==0 && c.Y==0), It.Is<char>(d => d==direction)))
            .Returns(new Coordinates(expectedX, expectedY));
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
    [MemberData(nameof(RoverTestData.GetBackwardCommandData), MemberType = typeof(RoverTestData))]
    public void BackwardCommandMovesRoverBackwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        //Arrange
        _driverMock
            .Setup(d => d.MoveBackward(It.Is<Coordinates>(c => c.X == 0 && c.Y == 0), It.Is<char>(d => d == direction)))
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

    [Theory]
    [MemberData(nameof(RoverTestData.GetTurnLeftCommandData), MemberType = typeof(RoverTestData))]
    public void TurnLeftMovesTheDirectionCounterClockwise(char startingDirection, char expectedDirection)
    {
        //Arrange
        _driverMock
            .Setup(d => d.TurnLeft(startingDirection))
            .Returns(expectedDirection);
        var rover = new Rover(_driverMock.Object)
        {
            Direction = startingDirection
        };
        var commands = new[] { 'l' };

        //Act
        rover.GetCommands(commands);

        //Assert
        Assert.Equal(expectedDirection, rover.Direction);
        Assert.Equal(0, rover.Position.X);
        Assert.Equal(0, rover.Position.Y);
    }

    [Theory]
    [MemberData(nameof(RoverTestData.GetTurnLeftCommandData), MemberType = typeof(RoverTestData))]
    public void TurnRightMovesTheDirectionClockwise(char startingDirection, char expectedDirection)
    {
        //Arrange
        _driverMock
            .Setup(d => d.TurnRight(startingDirection))
            .Returns(expectedDirection);
        var rover = new Rover(_driverMock.Object)
        {
            Direction = startingDirection
        };
        var commands = new[] { 'r' };

        //Act
        rover.GetCommands(commands);

        //Assert
        Assert.Equal(expectedDirection, rover.Direction);
        Assert.Equal(0, rover.Position.X);
        Assert.Equal(0, rover.Position.Y);
    }
}