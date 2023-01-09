using DafDev.Katas.MarsRover.Web.Tests.Data;

namespace DafDev.Katas.MarsRover.Web.Navigation;
public class DriverTests
{
    private readonly Driver _target;

    public DriverTests()
    {
        _target= new Driver();
    }

    [Theory]
    [MemberData(nameof(RoverTestData.GetForwardCommandData), MemberType = typeof(RoverTestData))]
    public void MoveForwardMovesRoverForwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        //Act
        var result = _target.MoveForward(new(0, 0), direction);

        //Assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);

    }

    [Theory]
    [MemberData(nameof(RoverTestData.GetBackwardCommandData), MemberType = typeof(RoverTestData))]
    public void MoveBackwardMovesRoverBackwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        //Act
        var result = _target.MoveBackward(new(0, 0), direction);

        //Assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);

    }

    [Theory]
    [MemberData(nameof(RoverTestData.GetTurnRightCommandData), MemberType = typeof(RoverTestData))]
    public void TurnRightMovesTheDirectionClockwise(char startingDirection, char expectedDirection)
    {
        //Act
        char result = _target.TurnRight(startingDirection);

        //Assert
        Assert.Equal(expectedDirection, result);
    }

    [Theory]
    [MemberData(nameof(RoverTestData.GetTurnLeftCommandData), MemberType = typeof(RoverTestData))]
    public void TurnLefttMovesTheDirectionClockwise(char startingDirection, char expectedDirection)
    {
        //Act
        char result = _target.TurnLeft(startingDirection);

        //Assert
        Assert.Equal(expectedDirection, result);
    }
}
