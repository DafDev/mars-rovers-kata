using DafDev.Katas.MarsRover.Application.Navigation.Exceptions;
using DafDev.Katas.MarsRover.Application.Navigation.Models;
using DafDev.Katas.MarsRover.Application.Navigation.Services;
using DafDev.Katas.MarsRover.Application.Tests.Data;

namespace DafDev.Katas.MarsRover.Application.Tests.Navigation.Services;
public class DriverTests
{
    private readonly DriverServices _sut = new();

    [Theory]
    [MemberData(nameof(DriverTestData.GetForwardCommandData), MemberType = typeof(DriverTestData))]
    public void MoveForwardMovesRoverForwardBy1UnitFromStartingPosition(CardinalDirections direction,
        Coordinates startingCoordinates, Coordinates expectedCoordinates)
    {
        //Act
        var result = _sut.MoveForward(startingCoordinates, direction);

        //Assert
        result.Should().Be(expectedCoordinates);
    }

    [Theory]
    [MemberData(nameof(DriverTestData.GetBackwardCommandData), MemberType = typeof(DriverTestData))]
    public void MoveBackwardMovesRoverBackwardBy1UnitFromStartingPosition(CardinalDirections direction,
        Coordinates startingCoordinates, Coordinates expectedCoordinates)
    {
        //Act
        var result = _sut.MoveBackward(startingCoordinates, direction);

        //Assert
        result.Should().Be(expectedCoordinates);
    }

    [Theory]
    [MemberData(nameof(DriverTestData.GetTurnRightCommandData), MemberType = typeof(DriverTestData))]
    public void TurnRightMovesTheDirectionClockwise(CardinalDirections startingDirection, CardinalDirections expectedDirection)
    {
        //Act
        var result = _sut.TurnRight(startingDirection);

        //Assert
        result.Should().Be(expectedDirection);
    }

    [Theory]
    [MemberData(nameof(DriverTestData.GetTurnLeftCommandData), MemberType = typeof(DriverTestData))]
    public void TurnLefttMovesTheDirectionCounterClockwise(CardinalDirections startingDirection, CardinalDirections expectedDirection)
    {
        //Act
        var result = _sut.TurnLeft(startingDirection);

        //Assert
        result.Should().Be(expectedDirection);
    }

    [Fact]
    public void MoveBackwardWithUnknownDirectionThrowsUnknownCardinalDirectionException()
    {
        //Arrange & Act
        var action = () => _sut.MoveBackward(new(0, 0), (CardinalDirections)5);

        //Assert
        action.Should().Throw<UnknownCardinalDirectionException>();
    }

    [Fact]
    public void MoveForwardWithUnknownDirectionThrowsUnknownCardinalDirectionException()
    {
        //Arrange & Act
        var action = () => _sut.MoveForward(new(0, 0), (CardinalDirections)5);

        //Assert
        action.Should().Throw<UnknownCardinalDirectionException>();
    }

    [Fact]
    public void TurnLeftdWithUnknownDirectionThrowsUnknownCardinalDirectionException()
    {
        //Arrange & Act
        var action = () => _sut.TurnLeft((CardinalDirections)5);

        //Assert
        action.Should().Throw<UnknownCardinalDirectionException>();
    }

    [Fact]
    public void TurnRightdWithUnknownDirectionThrowsUnknownCardinalDirectionException()
    {
        //Arrange & Act
        var action = () => _sut.TurnRight((CardinalDirections)5);

        //Assert
        action.Should().Throw<UnknownCardinalDirectionException>();
    }
}
