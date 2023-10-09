using DafDev.Katas.MarsRover.Application.Navigation.Exceptions;
using DafDev.Katas.MarsRover.Application.Navigation.Models;

namespace DafDev.Katas.MarsRover.Application.Navigation.Services;

public class DriverServices : IDriverServices
{
    public Coordinates MoveBackward(Coordinates coordinates, CardinalDirections direction) => direction switch
    {
        CardinalDirections.North when coordinates.Y == int.MaxValue => new(coordinates.X, int.MinValue),
        CardinalDirections.North => new(coordinates.X, coordinates.Y-1),
        CardinalDirections.East => new(coordinates.X-1, coordinates.Y),
        CardinalDirections.South when coordinates.Y == int.MinValue => new(coordinates.X, int.MaxValue),
        CardinalDirections.South => new(coordinates.X, coordinates.Y+1),
        CardinalDirections.West => new(coordinates.X + 1, coordinates.Y),
        _ => throw new UnknownCardinalDirectionException($"Cardinal direction {direction} does not exist"),
    };

    public Coordinates MoveForward(Coordinates coordinates, CardinalDirections direction) => direction switch
    {
        CardinalDirections.North when coordinates.Y == int.MinValue => new(coordinates.X, int.MaxValue),
        CardinalDirections.North => new(coordinates.X, coordinates.Y + 1),
        CardinalDirections.East => new(coordinates.X + 1, coordinates.Y),
        CardinalDirections.South when coordinates.Y == int.MaxValue => new(coordinates.X, int.MinValue),
        CardinalDirections.South => new(coordinates.X, coordinates.Y - 1),
        CardinalDirections.West => new(coordinates.X - 1, coordinates.Y),
        _ => throw new UnknownCardinalDirectionException($"Cardinal direction {direction} does not exist"),
    };

    public CardinalDirections TurnLeft(CardinalDirections startingDirection) => startingDirection switch
    {
        CardinalDirections.North => CardinalDirections.West,
        CardinalDirections.West => CardinalDirections.South,
        CardinalDirections.South => CardinalDirections.East,
        CardinalDirections.East => CardinalDirections.North,
        _ => throw new UnknownCardinalDirectionException($"Cardinal direction {startingDirection} does not exist")
    };

    public CardinalDirections TurnRight(CardinalDirections startingDirection) => startingDirection switch
    {
        CardinalDirections.North => CardinalDirections.East,
        CardinalDirections.East => CardinalDirections.South,
        CardinalDirections.South => CardinalDirections.West,
        CardinalDirections.West => CardinalDirections.North,
        _ => throw new UnknownCardinalDirectionException($"Cardinal direction {startingDirection} does not exist")
    };
}
