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
                case 'f': Position = _driver.MoveForward(Position,Direction); break;
                case 'b': Position = _driver.MoveBackward(Position, Direction); break;
                case 'l': Direction = _driver.TurnLeft(Direction); break;
                case 'r': Direction = _driver.TurnRight(Direction); break;
                default:
                    break;
            }
        }
    }
}
