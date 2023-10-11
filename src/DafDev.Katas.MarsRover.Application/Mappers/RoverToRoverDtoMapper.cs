using DafDev.Katas.MarsRover.Navigation.Application.Dtos;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Application.Mappers;
public class RoverToRoverDtoMapper : IRoverToRoverDtoMapper
{
    public RoverDto Map(Rover rover)
        => new()
        {
            Id = rover.Id,
            Position = new CoordinatesDto(rover.Position.X, rover.Position.Y),
            Direction = (CardinalDirectionsDto)rover.Direction
        };

    public IEnumerable<RoverDto> Map(IEnumerable<Rover> rovers) => rovers.Select(Map);
}
