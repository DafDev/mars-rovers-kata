namespace DafDev.Katas.MarsRover.Web.Navigation;

public class Driver : IDriver
{
    public Coordinates MoveBackward(Coordinates coordinates, char direction)
    {
        switch (direction)
        {
            case 'N': return new(coordinates.X, --coordinates.Y);
            case 'E': return new(--coordinates.X, coordinates.Y);
            case 'S': return new(coordinates.X, ++coordinates.Y); 
            case 'W': return new(++coordinates.X, coordinates.Y);
            default: return coordinates;
        }
    }

    public Coordinates MoveForward(Coordinates coordinates, char direction)
    {
        throw new NotImplementedException();
    }
}
