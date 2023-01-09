namespace DafDev.Katas.MarsRover.Web.Navigation;

public class Driver : IDriver
{
    public Coordinates MoveBackward(Coordinates coordinates, char direction) => direction switch
    {
        'N' => new(coordinates.X, --coordinates.Y),
        'E' => new(--coordinates.X, coordinates.Y),
        'S' => new(coordinates.X, ++coordinates.Y),
        'W' => new(++coordinates.X, coordinates.Y),
        _ => coordinates,
    };

    public Coordinates MoveForward(Coordinates coordinates, char direction)
    {
        if (coordinates.Y == int.MaxValue && direction == 'S')
            return new(coordinates.X, int.MinValue);
        return direction switch
        {
            'N' => new(coordinates.X, ++coordinates.Y),
            'E' => new(++coordinates.X, coordinates.Y),
            'S' => new(coordinates.X, --coordinates.Y),
            'W' => new(--coordinates.X, coordinates.Y),
            _ => coordinates,
        };
    }

    public char TurnLeft(char startingDirection) => startingDirection switch
    {
        'N' => 'W',
        'W' => 'S',
        'S' => 'E',
        'E' => 'N',
        _ => startingDirection
    };

    public char TurnRight(char startingDirection) => startingDirection switch
    {
        'N' => 'E',
        'E' => 'S',
        'S' => 'W',
        'W' => 'N',
        _ => startingDirection,
    };
}
