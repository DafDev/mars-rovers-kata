using Xunit;
using FluentAssertions;
using DafDev.Katas.MarsRover.Navigation.Domain.Exceptions;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using DafDev.Katas.MarsRover.Navigation.Domain.Services;
using DafDev.Katas.MarsRover.Navigation.Domain.Tests.Data;
using Moq;
using Microsoft.Extensions.Logging;

namespace DafDev.Katas.MarsRover.Navigation.Domain.Tests.Services;
public class DriverTests
{
    private readonly Mock<ILogger<DriverServices>> _logger = new();
    private readonly DriverServices _sut;

    public DriverTests() => _sut = new(_logger.Object);

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
        action
            .Should()
            .Throw<UnknownCardinalDirectionException>()
            .WithMessage("Cardinal direction 5 does not exist");
        _logger.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.Is<EventId>(eventId => eventId == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Cardinal direction 5 does not exist" && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void MoveForwardWithUnknownDirectionThrowsUnknownCardinalDirectionException()
    {
        //Arrange & Act
        var action = () => _sut.MoveForward(new(0, 0), (CardinalDirections)5);

        //Assert
        action
            .Should()
            .Throw<UnknownCardinalDirectionException>()
            .WithMessage("Cardinal direction 5 does not exist");
        _logger.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.Is<EventId>(eventId => eventId == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Cardinal direction 5 does not exist" && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void TurnLeftdWithUnknownDirectionThrowsUnknownCardinalDirectionException()
    {
        //Arrange & Act
        var action = () => _sut.TurnLeft((CardinalDirections)5);

        //Assert
        action
            .Should()
            .Throw<UnknownCardinalDirectionException>()
            .WithMessage("Cardinal direction 5 does not exist");
        _logger.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.Is<EventId>(eventId => eventId == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Cardinal direction 5 does not exist" && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public void TurnRightdWithUnknownDirectionThrowsUnknownCardinalDirectionException()
    {
        //Arrange & Act
        var action = () => _sut.TurnRight((CardinalDirections)5);

        //Assert
        action
            .Should()
            .Throw<UnknownCardinalDirectionException>()
            .WithMessage("Cardinal direction 5 does not exist");
        _logger.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.Is<EventId>(eventId => eventId == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "Cardinal direction 5 does not exist" && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}
