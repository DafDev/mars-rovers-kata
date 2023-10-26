using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using DafDev.Katas.MarsRover.Navigation.Domain.Repository;
using DafDev.Katas.MarsRover.Navigation.InMemoryInfrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace DafDev.Katas.MarsRover.Navigation.InMemoryInfrastructure.Repository;
public class InMemoryRoverRepository : IRoverRepository
{
    private readonly Dictionary<Guid, Rover> _rovers = new();
    private readonly ILogger<InMemoryRoverRepository> _logger;

    public InMemoryRoverRepository(ILogger<InMemoryRoverRepository> logger)
    {
        _logger = logger;
    }

    public async Task<Rover> Create(Rover? rover = null)
    {
        rover ??= new Rover();
        _rovers.Add(rover.RoverId, rover);
        return await Task.FromResult(rover);
    }

    public async Task Delete(Guid id)
    {
        if (!await Task.FromResult(_rovers.Remove(id)))
            LogAndThrowException($"rover w/ id: {id} does not exist");
    }

    public async Task<Rover> Get(Guid id)
        => !_rovers.TryGetValue(id, out var rover)
           ? LogAndThrowException($"rover w/ id: {id} does not exist")
           : await Task.FromResult(rover);

    public async Task<IEnumerable<Rover>> GetAll() => await Task.FromResult(_rovers.Values.AsEnumerable());

    public async Task<Rover> UpdateOrCreate(Rover rover)
    {
        if(!_rovers.ContainsKey(rover.RoverId))
            return await Create(rover);

        _rovers[rover.RoverId] = rover;
        return rover;
    }

    private Rover LogAndThrowException(string message)
    {
        _logger.LogError(message);
        throw new NonexistantRoverException(message);
    }
}
