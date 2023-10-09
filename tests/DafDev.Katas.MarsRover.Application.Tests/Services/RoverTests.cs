using DafDev.Katas.MarsRover.Application.Navigation.Mappers;
using DafDev.Katas.MarsRover.Application.Navigation.Models;
using DafDev.Katas.MarsRover.Application.Navigation.Repository;
using DafDev.Katas.MarsRover.Application.Tests.Data;
using DafDev.Katas.MarsRover.Application.Navigation.Services;
using Moq;
using DafDev.Katas.MarsRover.Application.Navigation.Exceptions;
using DafDev.Katas.MarsRover.InMemoryInfrastructure.Exceptions;

namespace DafDev.Katas.MarsRover.Application.Tests.Navigation.Services;

public class RoverTests
{
    private readonly RoverServices _sut;
    private readonly Mock<IDriverServices> _driver = new();
    private readonly Mock<IDriverCommandMapper> _mapper = new();
    private readonly Mock<IRoverRepository> _repository = new();

    public RoverTests() => _sut = new(_driver.Object, _mapper.Object, _repository.Object);



    [Theory]
    [MemberData(nameof(RoverTestData.GetLinearMovementCommandData), MemberType = typeof(RoverTestData))]
    public async Task LinearMovementCommandMovesRoverForwardOrBackwardBy1UnitFromStartingPosition(
        CardinalDirections direction,
        Coordinates startingCoordinates,
        Coordinates expectedCoordinates,
        DriverCommands mappedCommand,
        string command)
    {
        //Arrange
        SetupDriver(mappedCommand, direction, startingCoordinates, expectedCoordinates);

        var rover = new Rover()
        {
            Position = startingCoordinates,
            Direction = direction
        };

        var expectedRover = new Rover()
        {
            Position = expectedCoordinates,
            Direction = direction,
            Id = rover.Id
        };

        _repository
            .Setup(repo => repo.Get(It.IsAny<Guid>()))
            .ReturnsAsync(rover);
        _repository
            .Setup(repo => repo.Update(It.IsAny<Rover>()))
            .ReturnsAsync(expectedRover);

        _mapper
            .Setup(mapper => mapper.Map(It.IsAny<string>()))
            .Returns(new List<DriverCommands> { mappedCommand });

        //Act
        var result = await _sut.DriveRover(rover, command);

        //Assert
        result.Should().Be(expectedRover);
    }

    [Theory]
    [MemberData(nameof(RoverTestData.GetTurnCommandData), MemberType = typeof(RoverTestData))]
    public async Task TurnCommandTurnsRoverleftOrRightWithoutChangingStartingPosition(
        CardinalDirections direction,
        CardinalDirections expectedDirection,
        DriverCommands mappedCommand,
        string command)
    {
        //Arrange
        var startingCoordinates = new Coordinates(255, 2047);
        SetupDriver(mappedCommand, direction, expectedDirection: expectedDirection);

        var rover = new Rover()
        {
            Position = startingCoordinates,
            Direction = direction
        };
        var expectedRover = new Rover()
        {
            Position = startingCoordinates,
            Direction = expectedDirection,
            Id = rover.Id
        };

        _repository
            .Setup(repo => repo.Get(It.IsAny<Guid>()))
            .ReturnsAsync(rover);

        _repository
            .Setup(repo => repo.Update(It.IsAny<Rover>()))
            .ReturnsAsync(expectedRover);

        _mapper
            .Setup(mapper => mapper.Map(It.IsAny<string>()))
            .Returns(new List<DriverCommands> { mappedCommand });

        //Act
        var result = await _sut.DriveRover(rover, command);

        //Assert
        result.Should().Be(expectedRover);
    }

    [Fact]
    public async Task DriveRoverdWithUnknownCommandThrowsUnknownCardinalDirectionException()
    {
        // Arrange
        _mapper
            .Setup(mapper => mapper.Map(It.IsAny<string>()))
            .Returns(new List<DriverCommands> { (DriverCommands)5 });

        // Act
        var action = () => _sut.DriveRover(new Rover(), "a");

        // Assert
        await action.Should().ThrowAsync<UnknownDriverCommandException>();
    }

    [Fact]
    public async Task LandRoverCreatesNewRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        _repository
            .Setup(repo => repo.Create(rover))
            .ReturnsAsync(rover);

        // Act
        var result = await _sut.LandRover(rover);

        // Assert
        result.Should().Be(rover);
    }

    [Fact]
    public async Task GetRoverByIdShouldGetRightRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        _repository
            .Setup(repo => repo.Get(rover.Id))
            .ReturnsAsync(rover);

        // Act
        var result = await _sut.GetRoverById(rover.Id);

        // Assert
        result.Should().Be(rover);
    }

    [Fact]
    public async Task GetAllRoversShouldGetAllRoversInMemory()
    {
        // Arrange
        var rover = new Rover();
        var rover2 = new Rover();
        var rovers = new List<Rover> { rover, rover2 };
        _repository
            .Setup(repo => repo.GetAll())
            .ReturnsAsync(rovers);

        // Act
        var result = await _sut.GetAllRovers();

        // Assert
        result.Should().BeEquivalentTo(rovers);
    }

    [Fact]
    public async Task DecommissionRoverShouldDeleteRightRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        _repository
            .SetupSequence(repo => repo.Get(rover.Id))
            .ReturnsAsync(rover)
            .ThrowsAsync(new NonexistantRoverException("no"));

        // Act
        var roverToDelete = await _sut.GetRoverById(rover.Id);
        await _sut.DecommissionRover(roverToDelete.Id);

        var result = () => _sut.GetRoverById(rover.Id);

        // Assert
        await result.Should().ThrowAsync<NonexistantRoverException>();
    }

    private void SetupDriver(
        DriverCommands mappedCommand,
        CardinalDirections direction,
        Coordinates? startingCoordinates = null,
        Coordinates? expectedCoordinates = null,
        CardinalDirections expectedDirection = CardinalDirections.North)
    {

        startingCoordinates ??= new Coordinates(0, 0);

        expectedCoordinates ??= new Coordinates(0, 0);

        switch (mappedCommand)
        {
            case DriverCommands.Forward:
                _driver
                   .Setup(d => d.MoveForward(startingCoordinates, direction))
                   .Returns(expectedCoordinates);
                break;
            case DriverCommands.Backward:
                _driver
                    .Setup(d => d.MoveBackward(startingCoordinates, direction))
                    .Returns(expectedCoordinates);
                break;
            case DriverCommands.Left:
                _driver
                    .Setup(d => d.TurnLeft(direction))
                    .Returns(expectedDirection);
                break;
            case DriverCommands.Right:
                _driver
                    .Setup(d => d.TurnRight(direction))
                    .Returns(expectedDirection);
                break;
            default:
                break;
        }

    }
}