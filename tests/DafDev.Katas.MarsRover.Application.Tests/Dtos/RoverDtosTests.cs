using DafDev.Katas.MarsRover.Navigation.Application.Dtos;

namespace DafDev.Katas.MarsRover.Navigation.Application.Tests.Dtos;
public class RoverDtosTests
{
    [Fact]
    public void ToDomainReturnsModelFromDomain()
    {
        // Arrange
        var roverDto = new RoverDto()
        {
            Position = new CoordinatesDto(1, 1),
            Direction = CardinalDirectionsDto.North,
            Id = new Guid()
        };
        var expected = new Rover()
        {
            Position = new Coordinates(1, 1),
            Direction = CardinalDirections.North,
            Id = roverDto.Id
        };


        // Act
        var rover = roverDto.ToDomain();

        // Assert
        rover.Should().BeEquivalentTo(expected);
    }
}
