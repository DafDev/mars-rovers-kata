using DafDev.Katas.MarsRover.Application.Navigation.Mappers;
using DafDev.Katas.MarsRover.Application.Navigation.Models;
using DafDev.Katas.MarsRover.Application.Navigation.Repository;
using DafDev.Katas.MarsRover.Application.Tests.Data;
using DafDev.Katas.MarsRover.Application.Navigation.Services;
using Moq;
using DafDev.Katas.MarsRover.Application.Navigation.Exceptions;

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
    public void LinearMovementCommandMovesRoverForwardOrBackwardBy1UnitFromStartingPosition(
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
        _repository
            .Setup(repo => repo.Get(It.IsAny<Guid>()))
            .Returns(rover);

        _mapper
            .Setup(mapper => mapper.Map(It.IsAny<string>()))
            .Returns(new List<DriverCommands> { mappedCommand });

        //Act
        var result = _sut.DriveRover(rover, command);

        //Assert
        result.Direction.Should().Be(direction);
        result.Position.Should().Be(expectedCoordinates);
    }

    [Theory]
    [MemberData(nameof(RoverTestData.GetTurnCommandData), MemberType = typeof(RoverTestData))]
    public void TurnCommandTurnsRoverleftOrRightWithoutChangingStartingPosition(
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
        _repository
            .Setup(repo => repo.Get(It.IsAny<Guid>()))
            .Returns(rover);

        _mapper
            .Setup(mapper => mapper.Map(It.IsAny<string>()))
            .Returns(new List<DriverCommands> { mappedCommand });

        //Act
        var result = _sut.DriveRover(rover, command);

        //Assert
        result.Direction.Should().Be(expectedDirection);
        result.Position.Should().Be(startingCoordinates);
    }

    [Fact]
    public void DriveRoverdWithUnknownCommandThrowsUnknownCardinalDirectionException()
    {
        // Arrange
        _mapper
            .Setup(mapper => mapper.Map(It.IsAny<string>()))
            .Returns(new List<DriverCommands> { (DriverCommands)5 });

        // Act
        var action = () => _sut.DriveRover(new Rover(), "a" );

        // Assert
        action.Should().Throw<UnknownDriverCommandException>();
    }

    [Fact]
    public void LandRoverCreatesNewRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        _repository
            .Setup(repo => repo.Create(rover))
            .Returns(rover);

        // Act
        var result = _sut.LandRover(rover);

        // Assert
        result.Should().Be(rover);
    }

    [Fact]
    public void GetRoverByIdShouldGetRightRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        _repository
            .Setup(repo => repo.Get(rover.Id))
            .Returns(rover);
        // Act
        var result = _sut.GetRoverById(rover.Id);
        // Assert
        result.Should().Be(rover);
    }

    [Fact]
    public void GetAllRoversShouldGetAllRoversInMemory()
    {
        // Arrange
        var rover = new Rover();
        var rover2 = new Rover();
        var rovers = new List<Rover> { rover, rover2 };
        _repository
            .Setup(repo => repo.GetAll())
            .Returns(rovers);
        // Act
        var result = _sut.GetAllRovers();
        // Assert
        result.Should().BeEquivalentTo(rovers);
    }

    [Fact]
    public void DecommissionRoverShouldDeleteRightRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        _repository
            .Setup(repo => repo.Get(rover.Id))
            .Returns(rover);
        _repository
            .SetupSequence(repo => repo.Get(rover.Id))
            .Returns(rover)
            .Throws<Exception>();//NonExistantRoverException
        // Act
        var roverToDelete = _sut.GetRoverById(rover.Id);
        _sut.DecommissionRover(roverToDelete.Id);
        var result = () => _sut.GetRoverById(rover.Id);
        // Assert
        result.Should().Throw<Exception>();
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