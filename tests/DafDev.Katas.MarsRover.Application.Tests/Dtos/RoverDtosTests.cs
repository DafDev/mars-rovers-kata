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
            RoverId = new Guid()
        };
        var expected = new Rover()
        {
            Position = new Coordinates(1, 1),
            Direction = CardinalDirections.North,
            RoverId = roverDto.RoverId
        };


        // Act
        var rover = roverDto.ToDomain();

        // Assert
        rover.Should().BeEquivalentTo(expected);
    }
}
