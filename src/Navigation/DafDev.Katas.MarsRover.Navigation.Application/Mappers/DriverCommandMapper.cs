using DafDev.Katas.MarsRover.Navigation.Application.Exceptions;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using Microsoft.Extensions.Logging;

namespace DafDev.Katas.MarsRover.Navigation.Application.Mappers;

public class DriverCommandMapper : IDriverCommandMapper
{
    private readonly ILogger<DriverCommandMapper> _logger;

    public DriverCommandMapper(ILogger<DriverCommandMapper> logger)
    {
        _logger = logger;
    }

    public IEnumerable<DriverCommands> Map(string commands)
    {
        foreach (char command in commands)
        {
            yield return Map(command);
        }
    }

    public DriverCommands Map(char command) => command switch
    {
        'f' or 'F' => DriverCommands.Forward,
        'b' or 'B' => DriverCommands.Backward,
        'l' or 'L' => DriverCommands.Left,
        'r' or 'R' => DriverCommands.Right,
        _ => LogAndThrowException($"'{command}' command does not exist")
    };

    private DriverCommands LogAndThrowException(string message)
    {
        _logger.LogError(message);
        throw new UnknownDriverCommandException(message);
    }
}