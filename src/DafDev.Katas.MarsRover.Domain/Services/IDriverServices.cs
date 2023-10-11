using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Domain.Services;

public interface IDriverServices
{
    Coordinates MoveForward(Coordinates coordinates, CardinalDirections direction);
    Coordinates MoveBackward(Coordinates coordinates, CardinalDirections direction);
    CardinalDirections TurnRight(CardinalDirections startingDirection);
    CardinalDirections TurnLeft(CardinalDirections startingDirection);
}
