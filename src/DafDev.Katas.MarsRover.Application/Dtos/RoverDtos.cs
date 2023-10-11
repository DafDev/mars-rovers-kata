using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Application.Dtos;
public class RoverDto
{
    public CoordinatesDto Position { get; set; }
    public CardinalDirectionsDto Direction { get; set; }
    public Guid Id { get; init; }

    public Rover ToDomain()
        => new()
        {
            Position = new Coordinates(Position.X, Position.Y),
            Direction = (CardinalDirections)Direction,
            Id = Id,
        };
}
