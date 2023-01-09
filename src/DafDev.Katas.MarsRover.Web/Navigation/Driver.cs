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
        return direction switch
        {
            'N' when coordinates.Y == int.MinValue => new(coordinates.X, int.MaxValue),
            'N' => new(coordinates.X, ++coordinates.Y),
            'E' => new(++coordinates.X, coordinates.Y),
            'S' when coordinates.Y == int.MaxValue => new(coordinates.X, int.MinValue),
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
