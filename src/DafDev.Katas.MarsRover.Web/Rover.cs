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
            if (command == 'f')
                StartingPoint.Y++;
        }
    }

    public Rover Init() => new()
    {
        StartingPoint= new Point(0, 0),
        Direction = 'N',
    };
}
