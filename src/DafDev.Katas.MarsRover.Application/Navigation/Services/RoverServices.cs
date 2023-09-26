using DafDev.Katas.MarsRover.Application.Navigation.Repository;
using DafDev.Katas.MarsRover.Application.Navigation.Mappers;
using DafDev.Katas.MarsRover.Application.Navigation.Models;
using DafDev.Katas.MarsRover.Application.Navigation.Exceptions;

namespace DafDev.Katas.MarsRover.Application.Navigation.Services;
public class RoverServices : IRoverServices
{
    private readonly IDriverServices _driver;
    private readonly IDriverCommandMapper _driverDirectionMapper;
    private readonly IRoverRepository _roverRepository;

    public RoverServices(IDriverServices driver, IDriverCommandMapper driverDirectionMapper, IRoverRepository roverRepository)
    {
        _driver = driver;
        _driverDirectionMapper = driverDirectionMapper;
        _roverRepository = roverRepository;
    }


    public Rover DriveRover(Rover rover, string commands)
    {
        var mappedCommands = _driverDirectionMapper.Map(commands).ToList();

        foreach (var mappedCommand in mappedCommands)
        {
            switch (mappedCommand)
            {
                case DriverCommands.Forward: rover.Position = _driver.MoveForward(rover.Position, rover.Direction); break;
                case DriverCommands.Backward: rover.Position = _driver.MoveBackward(rover.Position, rover.Direction); break;
                case DriverCommands.Left: rover.Direction = _driver.TurnLeft(rover.Direction); break;
                case DriverCommands.Right: rover.Direction = _driver.TurnRight(rover.Direction); break;
                default: throw new UnknownDriverCommandException($"unknown {mappedCommand} driver command");
                    
            }
        }

        return rover;
    }

    public Rover GetRoverById(Guid id) => _roverRepository.Get(id);

    public Rover LandRover(Rover? rover = null) => _roverRepository.Create(rover);
    public void DecommissionRover(Guid id) => _roverRepository.Delete(id);

    public IEnumerable<Rover> GetAllRovers() => _roverRepository.GetAll();
}
