using DafDev.Katas.MarsRover.Web.Navigation;

namespace DafDev.Katas.MarsRover.Web;

public class Rover
{
    public Coordinates Position { get; set; }
    public char Direction { get; set; }
    private readonly IDriver _driver;

    public Rover(IDriver driver)
    {
        _driver = driver;
        Position = new Coordinates(0, 0);
        Direction = 'N';
    }

    public void GetCommands(char[] commands)
    {
        foreach (var command in commands)
        {
            switch (command)
            {
                case 'f': MoveForward(Direction); break;
                case 'b': Position = _driver.MoveBackward(Position, Direction); break;
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
}
