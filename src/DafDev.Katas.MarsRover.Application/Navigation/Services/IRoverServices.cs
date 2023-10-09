
using DafDev.Katas.MarsRover.Application.Navigation.Models;

namespace DafDev.Katas.MarsRover.Application.Navigation.Services;
public interface IRoverServices
{
    Rover DriveRover(Rover rover, string commands);
    Rover LandRover(Rover? rover = null);
    Rover GetRoverById(Guid id);
    IEnumerable<Rover> GetAllRovers();
    void DecommissionRover(Guid id);
}