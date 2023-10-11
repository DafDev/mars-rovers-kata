using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Application.Mappers;

public interface IDriverCommandMapper
{
    DriverCommands Map(char direction);
    IEnumerable<DriverCommands> Map(string directions);
}
