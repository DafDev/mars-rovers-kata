namespace DafDev.Katas.MarsRover.Navigation.Domain.Models;

public class Rover
{
    public Coordinates Position { get; set; }
    public CardinalDirections Direction { get; set; }
    public Guid Id { get; init; }

    public Rover()
    {
        Position = new Coordinates(0, 0);
        Direction = CardinalDirections.North;
        Id = Guid.NewGuid();
    }
}
