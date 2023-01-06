namespace DafDev.Katas.MarsRover.Web;

public class Rover
{
    public int X { get; set; }
    public int Y { get; set; }
    public char Direction { get; set; }

    public Rover Init() => new()
    {
        X = 0,
        Y = 0,
        Direction = 'N',
    };
}
