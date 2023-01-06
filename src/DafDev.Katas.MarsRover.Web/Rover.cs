using System.Drawing;

namespace DafDev.Katas.MarsRover.Web;

public class Rover
{
    public Point StartingPoint { get; set; }
    public char Direction { get; set; }

    public void GetCommands(char[] commands)
    {
        throw new NotImplementedException();
    }

    public Rover Init() => new()
    {
        StartingPoint= new Point(0, 0),
        Direction = 'N',
    };
}
