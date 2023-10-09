
using DafDev.Katas.MarsRover.Application.Navigation.Models;

namespace DafDev.Katas.MarsRover.Application.Navigation.Services;
public interface IRoverServices
{
    Task<Rover> LandRover(Rover? rover = null);
    Task DecommissionRover(Guid id);
    Task<IEnumerable<Rover>> GetAllRovers();
    Task<Rover> DriveRover(Rover rover, string commands);
    Task<Rover> GetRoverById(Guid id);
}