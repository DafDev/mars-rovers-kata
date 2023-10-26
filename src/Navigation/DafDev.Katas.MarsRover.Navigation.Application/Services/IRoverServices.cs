using DafDev.Katas.MarsRover.Navigation.Application.Dtos;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Application.Services;
public interface IRoverServices
{
    Task<RoverDto> LandRover(Rover? Rover = null);
    Task DecommissionRover(Guid id);
    Task<IEnumerable<RoverDto>> GetAllRovers();
    Task<RoverDto> DriveRover(Rover Rover, string commands); //ToDo perhaps only use guid to get rover by id later in method
    Task<RoverDto> GetRoverById(Guid id);
    Task DecommissionAllRovers();
}