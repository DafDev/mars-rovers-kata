using DafDev.Katas.MarsRover.Web.Navigation;

namespace DafDev.Katas.MarsRover.Web;

public class Rover
{
    public Coordinates Position { get; set; }
    public char Direction { get; set; }

    public void GetCommands(char[] commands)
    {
        foreach (var command in commands)
        {
            switch (command)
            {
                case 'f': MoveForward(Direction); break;
                case 'b': MoveBackward(Direction); break;
                default:
                    break;
            }
        }
    }

    private void MoveForward(char direction)
    {
        switch (direction)
        {
            case 'N': Position.Y++; break;
            case 'E': Position.X++; break;
            case 'S': Position.Y--; break;
            case 'W': Position.X--; break;
            default: break;
        }
    }

    private void MoveBackward(char direction)
    {
        switch (direction)
        {
            case 'N': Position.Y--; break;
            case 'E': Position.X--; break;
            case 'S': Position.Y++; break;
            case 'W': Position.X++; break;
            default: break;
        }
    }

    public Rover Init() => new()
    {
        Position= new Coordinates(0, 0),
        Direction = 'N',
    };
}
