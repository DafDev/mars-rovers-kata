using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using DafDev.Katas.MarsRover.Navigation.Domain.Repository;
using DafDev.Katas.MarsRover.Navigation.Application.Tests.Data;
using Moq;
using DafDev.Katas.MarsRover.Navigation.Application.Exceptions;
using DafDev.Katas.MarsRover.Navigation.InMemoryInfrastructure.Exceptions;
using DafDev.Katas.MarsRover.Navigation.Application.Services;
using DafDev.Katas.MarsRover.Navigation.Application.Mappers;
using DafDev.Katas.MarsRover.Navigation.Domain.Services;
using DafDev.Katas.MarsRover.Navigation.Application.Dtos;
using Microsoft.Extensions.Logging;

namespace DafDev.Katas.MarsRover.Navigation.Application.Tests.Services;

public class RoverTests
{
    private readonly RoverServices _sut;
    private readonly Mock<IDriverServices> _driver = new();
    private readonly Mock<IDriverCommandMapper> _mapper = new();
    private readonly Mock<IRoverRepository> _repository = new();
    private readonly Mock<IRoverToRoverDtoMapper> _roverToDtoMapper = new();
    private readonly Mock<ILogger<RoverServices>> _logger = new();

    public RoverTests()
        => _sut = new(
            _driver.Object,
            _mapper.Object,
            _repository.Object,
            _roverToDtoMapper.Object,
            _logger.Object);



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

        var updatedRover = new Rover()
        {
            Position = expectedCoordinates,
            Direction = direction,
            Id = rover.Id
        };
        var expectedRover = new RoverDto()
        {
            Direction = (CardinalDirectionsDto)updatedRover.Direction,
            Position = new CoordinatesDto(updatedRover.Position.X, updatedRover.Position.Y),
            Id = updatedRover.Id
        };

        SetupRepository(rover, updatedRover);
        SetupRoverToDtoMapper(updatedRover, expectedRover);

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
        var updatedRover = new Rover()
        {
            Position = startingCoordinates,
            Direction = expectedDirection,
            Id = rover.Id
        };
        var expectedRover = new RoverDto()
        {
            Direction = (CardinalDirectionsDto)updatedRover.Direction,
            Position = new CoordinatesDto(updatedRover.Position.X, updatedRover.Position.Y),
            Id = updatedRover.Id
        };

        SetupRepository(rover, updatedRover);
        SetupRoverToDtoMapper(updatedRover, expectedRover);

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
        _logger.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.Is<EventId>(eventId => eventId == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "unknown 5 driver command" && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task LandRoverCreatesNewRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        var expectedRover = new RoverDto()
        {
            Direction = (CardinalDirectionsDto)rover.Direction,
            Position = new CoordinatesDto(rover.Position.X, rover.Position.Y),
            Id = rover.Id
        };

        _repository
            .Setup(repo => repo.Create(rover))
            .ReturnsAsync(rover);
        SetupRoverToDtoMapper(rover, expectedRover);

        // Act
        var result = await _sut.LandRover(rover);

        // Assert
        result.Should().Be(expectedRover);
    }

    [Fact]
    public async Task GetRoverByIdShouldGetRightRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        var expectedRover = new RoverDto()
        {
            Direction = (CardinalDirectionsDto) rover.Direction,
            Position = new CoordinatesDto(rover.Position.X, rover.Position.Y),
            Id = rover.Id
        };

        _repository
            .Setup(repo => repo.Get(rover.Id))
            .ReturnsAsync(rover);
        SetupRoverToDtoMapper(rover, expectedRover);

        // Act
        var result = await _sut.GetRoverById(rover.Id);

        // Assert
        result.Should().Be(expectedRover);
    }

    [Fact]
    public async Task GetAllRoversShouldGetAllRoversInMemory()
    {
        // Arrange
        var rover = new Rover();
        var rovers = new List<Rover> { rover };
        var expectedRovers = new List<RoverDto>
        {
            new()
            {
                Direction = (CardinalDirectionsDto) rover.Direction,
                Position = new CoordinatesDto(rover.Position.X, rover.Position.Y),
                Id = rover.Id
            }
        };

        _repository
            .Setup(repo => repo.GetAll())
            .ReturnsAsync(rovers);
        _roverToDtoMapper
            .Setup(mapper => mapper.Map(It.IsAny<List<Rover>>()))
            .Returns(expectedRovers);

        // Act
        var result = await _sut.GetAllRovers();

        // Assert
        result.Should().BeEquivalentTo(expectedRovers);
    }

    [Fact]
    public async Task DecommissionRoverShouldDeleteRightRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        var roverDto = new RoverDto()
        {
            Direction = (CardinalDirectionsDto)rover.Direction,
            Position = new CoordinatesDto(rover.Position.X, rover.Position.Y),
            Id = rover.Id
        };

        _repository
            .SetupSequence(repo => repo.Get(rover.Id))
            .ReturnsAsync(rover)
            .ThrowsAsync(new NonexistantRoverException("no"));
        SetupRoverToDtoMapper(rover, roverDto);

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

    private void SetupRepository(Rover rover, Rover updatedRover)
    {
        _repository
            .Setup(repo => repo.Get(It.IsAny<Guid>()))
            .ReturnsAsync(rover);
        _repository
            .Setup(repo => repo.Update(It.Is<Rover>(rover
                => rover.Id == updatedRover.Id
                    && rover.Position == updatedRover.Position
                    && rover.Direction == updatedRover.Direction)))
            .ReturnsAsync(updatedRover);
    }

    private void SetupRoverToDtoMapper(Rover roverToMap, RoverDto mappedRover)
    {
        _roverToDtoMapper
            .Setup(mapper => mapper.Map(It.Is<Rover>(rover
                => rover.Id == roverToMap.Id
                && rover.Position == roverToMap.Position
                && rover.Direction == roverToMap.Direction)))
            .Returns(mappedRover);
    }
}