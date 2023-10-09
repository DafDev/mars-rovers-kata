using DafDev.Katas.MarsRover.Application.Navigation.Models;

namespace DafDev.Katas.MarsRover.Application.Navigation.Services;

public interface IDriverServices
{
    Coordinates MoveForward(Coordinates coordinates, CardinalDirections direction);
    Coordinates MoveBackward(Coordinates coordinates, CardinalDirections direction);
    CardinalDirections TurnRight(CardinalDirections startingDirection);
    CardinalDirections TurnLeft(CardinalDirections startingDirection);
}
