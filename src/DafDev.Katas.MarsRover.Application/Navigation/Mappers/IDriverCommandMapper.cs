using DafDev.Katas.MarsRover.Application.Navigation.Models;

namespace DafDev.Katas.MarsRover.Application.Navigation.Mappers;

public interface IDriverCommandMapper
{
    DriverCommands Map(char direction);
    IEnumerable<DriverCommands> Map(string directions);
}
