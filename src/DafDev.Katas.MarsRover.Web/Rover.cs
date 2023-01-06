using DafDev.Katas.MarsRover.Web.Navigation;

namespace DafDev.Katas.MarsRover.Web;

public class Rover
{
    public Coordinates StartingPoint { get; set; }
    public char Direction { get; set; }

    public void GetCommands(char[] commands)
    {
        foreach (var command in commands)
        {
            switch (command)
            {
                case 'f': MoveForward(Direction); break;
                default:
                    break;
            }
        }
    }

    private void MoveForward(char direction)
    {
        switch (direction)
        {
            case 'N': StartingPoint.Y++; break;
            case 'E': StartingPoint.X++; break;
            default: break;
        }
    }

    public Rover Init() => new()
    {
        StartingPoint= new Point(0, 0),
        Direction = 'N',
    };
}
