using DafDev.Katas.MarsRover.Navigation.Application.Dtos;
using DafDev.Katas.MarsRover.Navigation.Application.Mappers;

namespace DafDev.Katas.MarsRover.Navigation.Application.Tests.Mappers;
public class RoverToRoverDtoMapperTests
{
    [Fact]
    public void MapShouldReturnMappedRovers()
    {
        // Arrange
        var roverDto = new RoverDto()
        {
            Position = new CoordinatesDto(1, 1),
            Direction = CardinalDirectionsDto.North,
            Id = new Guid()
        };
        var expected = new List<RoverDto> { roverDto };
        var rover = new Rover()
        {
            Position = new Coordinates(1, 1),
            Direction = CardinalDirections.North,
            Id = roverDto.Id
        };
        var roversToMap = new List<Rover> { rover };
        var sut = new RoverToRoverDtoMapper();

        // Act
        var result = sut.Map(roversToMap);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void MapShouldReturnMappedRover()
    {
        // Arrange
        var rover = new Rover()
        {
            Position = new Coordinates(1, 1),
            Direction = CardinalDirections.North,
            Id = new Guid(),
        };
        var expected = new RoverDto()
        {
            Position = new CoordinatesDto(1, 1),
            Direction = CardinalDirectionsDto.North,
            Id = rover.Id
        };
        
        var sut = new RoverToRoverDtoMapper();

        // Act
        var result = sut.Map(rover);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}
