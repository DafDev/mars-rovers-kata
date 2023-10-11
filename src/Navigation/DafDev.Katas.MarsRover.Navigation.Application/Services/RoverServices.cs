using DafDev.Katas.MarsRover.Navigation.Domain.Repository;
using DafDev.Katas.MarsRover.Navigation.Application.Exceptions;
using DafDev.Katas.MarsRover.Navigation.Application.Mappers;
using DafDev.Katas.MarsRover.Navigation.Domain.Services;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using DafDev.Katas.MarsRover.Navigation.Application.Dtos;
using Microsoft.Extensions.Logging;

namespace DafDev.Katas.MarsRover.Navigation.Application.Services;
public class RoverServices : IRoverServices
{
    private readonly IDriverServices _driver;
    private readonly IDriverCommandMapper _driverDirectionMapper;
    private readonly IRoverRepository _roverRepository;
    private readonly IRoverToRoverDtoMapper _roverToRoverDtoMapper;
    private readonly ILogger<RoverServices> _logger;

    public RoverServices(
        IDriverServices driver,
        IDriverCommandMapper driverDirectionMapper,
        IRoverRepository roverRepository,
        IRoverToRoverDtoMapper roverToRoverDtoMapper,
        ILogger<RoverServices> logger)
    {
        _driver = driver;
        _driverDirectionMapper = driverDirectionMapper;
        _roverRepository = roverRepository;
        _roverToRoverDtoMapper = roverToRoverDtoMapper;
        _logger = logger;
    }


    public async Task<RoverDto> DriveRover(Rover rover, string commands)
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
                default:
                    _logger.LogError("unknown {mappedCommand} driver command", mappedCommand);
                    throw new UnknownDriverCommandException($"unknown {mappedCommand} driver command");

            }
        }

        var updatedRover = await _roverRepository.Update(rover);
        return _roverToRoverDtoMapper.Map(updatedRover);
    }

    public async Task<RoverDto> GetRoverById(Guid id) => _roverToRoverDtoMapper.Map(await _roverRepository.Get(id));

    public async Task<RoverDto> LandRover(Rover? rover = null) => _roverToRoverDtoMapper.Map(await _roverRepository.Create(rover));
    public async Task DecommissionRover(Guid id) => await _roverRepository.Delete(id);

    public async Task<IEnumerable<RoverDto>> GetAllRovers() => _roverToRoverDtoMapper.Map(await _roverRepository.GetAll());
}
