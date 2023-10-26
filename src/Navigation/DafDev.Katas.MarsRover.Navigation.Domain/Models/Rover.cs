namespace DafDev.Katas.MarsRover.Navigation.Domain.Models;

public class Rover
{
    public string Id { get; set; }

    public Coordinates Position { get; set; }
    public CardinalDirections Direction { get; set; }
    public Guid RoverId { get; init; }

    public Rover()
    {
        Position = new Coordinates(0, 0);
        Direction = CardinalDirections.North;
        RoverId = Guid.NewGuid();
    }
}
