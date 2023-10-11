using DafDev.Katas.MarsRover.Navigation.Application.Dtos;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Application.Mappers;
public interface IRoverToRoverDtoMapper
{
    RoverDto Map(Rover rover);
    IEnumerable<RoverDto> Map(IEnumerable<Rover> enumerable);
}
