using DafDev.Katas.MarsRover.Navigation.Domain.Exceptions;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DafDev.Katas.MarsRover.Navigation.Domain.Services;

public class DriverServices : IDriverServices
{
    private readonly ILogger<DriverServices> _logger;

    public DriverServices(ILogger<DriverServices> logger)
    {
        _logger = logger;
    }

    public Coordinates MoveBackward(Coordinates coordinates, CardinalDirections direction) => direction switch
    {
        CardinalDirections.North when coordinates.Y == int.MaxValue => new(coordinates.X, int.MinValue),
        CardinalDirections.North => new(coordinates.X, coordinates.Y - 1),
        CardinalDirections.East => new(coordinates.X - 1, coordinates.Y),
        CardinalDirections.South when coordinates.Y == int.MinValue => new(coordinates.X, int.MaxValue),
        CardinalDirections.South => new(coordinates.X, coordinates.Y + 1),
        CardinalDirections.West => new(coordinates.X + 1, coordinates.Y),
        _ => LogAndThrowExceptionForLinearMovement($"Cardinal direction {direction} does not exist"),
    };

    public Coordinates MoveForward(Coordinates coordinates, CardinalDirections direction) => direction switch
    {
        CardinalDirections.North when coordinates.Y == int.MinValue => new(coordinates.X, int.MaxValue),
        CardinalDirections.North => new(coordinates.X, coordinates.Y + 1),
        CardinalDirections.East => new(coordinates.X + 1, coordinates.Y),
        CardinalDirections.South when coordinates.Y == int.MaxValue => new(coordinates.X, int.MinValue),
        CardinalDirections.South => new(coordinates.X, coordinates.Y - 1),
        CardinalDirections.West => new(coordinates.X - 1, coordinates.Y),
        _ => LogAndThrowExceptionForLinearMovement($"Cardinal direction {direction} does not exist"),
    };

    public CardinalDirections TurnLeft(CardinalDirections startingDirection) => startingDirection switch
    {
        CardinalDirections.North => CardinalDirections.West,
        CardinalDirections.West => CardinalDirections.South,
        CardinalDirections.South => CardinalDirections.East,
        CardinalDirections.East => CardinalDirections.North,
        _ => LogAndThrowExceptionFroCircularMovement($"Cardinal direction {startingDirection} does not exist")
    };

    public CardinalDirections TurnRight(CardinalDirections startingDirection) => startingDirection switch
    {
        CardinalDirections.North => CardinalDirections.East,
        CardinalDirections.East => CardinalDirections.South,
        CardinalDirections.South => CardinalDirections.West,
        CardinalDirections.West => CardinalDirections.North,
        _ => LogAndThrowExceptionFroCircularMovement($"Cardinal direction {startingDirection} does not exist")
    };

    private Coordinates LogAndThrowExceptionForLinearMovement(string message)
    {
        _logger.LogError(message);
        throw new UnknownCardinalDirectionException(message);
    }

    private CardinalDirections LogAndThrowExceptionFroCircularMovement(string message)
    {
        _logger.LogError(message);
        throw new UnknownCardinalDirectionException(message);
    }
}
