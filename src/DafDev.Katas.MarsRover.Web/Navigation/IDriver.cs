namespace DafDev.Katas.MarsRover.Web.Navigation;

public interface IDriver
{
    Coordinates MoveForward(Coordinates coordinates, char direction);
    Coordinates MoveBackward(Coordinates coordinates, char direction);
}
