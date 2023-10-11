using DafDev.Katas.MarsRover.Navigation.Application.Exceptions;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Application.Mappers;

public class DriverCommandMapper : IDriverCommandMapper
{
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
        _ => throw new UnknownDriverCommandException($"'{command}' command does not exist")
    };
}