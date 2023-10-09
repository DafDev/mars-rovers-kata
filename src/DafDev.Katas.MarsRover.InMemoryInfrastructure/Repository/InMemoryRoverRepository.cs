using DafDev.Katas.MarsRover.Application.Navigation.Models;
using DafDev.Katas.MarsRover.Application.Navigation.Repository;
using DafDev.Katas.MarsRover.InMemoryInfrastructure.Exceptions;

namespace DafDev.Katas.MarsRover.InMemoryInfrastructure.Repository;
public class InMemoryRoverRepository : IRoverRepository
{
    private readonly Dictionary<Guid, Rover> _rovers = new();

    public async Task<Rover> Create(Rover? rover = null)
    {
        rover ??= new Rover();
        _rovers.Add(rover.Id, rover);
        return await Task.FromResult(rover);
    }

    public async Task Delete(Guid id)
    {
        if (!await Task.FromResult(_rovers.Remove(id))) 
            throw new NonexistantRoverException($"rover w/ id: {id} does not exist");
    }

    public async Task<Rover> Get(Guid id)
        => !await Task.FromResult(_rovers.TryGetValue(id, out var rover))
           ? throw new NonexistantRoverException($"rover w/ id: {id} does not exist")
           : rover;

    public async Task<IEnumerable<Rover>> GetAll() => await Task.FromResult(_rovers.Values.AsEnumerable());

    public async Task<Rover> Update(Rover rover)
    {
        if(!_rovers.ContainsKey(rover.Id))
            return await Create(rover);

        _rovers[rover.Id] = rover;
        return rover;
    }
}
