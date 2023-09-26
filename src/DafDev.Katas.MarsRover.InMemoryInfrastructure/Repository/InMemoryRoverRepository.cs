using DafDev.Katas.MarsRover.Application.Navigation.Models;
using DafDev.Katas.MarsRover.Application.Navigation.Repository;
using DafDev.Katas.MarsRover.InMemoryInfrastructure.Exceptions;

namespace DafDev.Katas.MarsRover.InMemoryInfrastructure.Repository;
public class InMemoryRoverRepository : IRoverRepository
{
    private readonly Dictionary<Guid, Rover> _rovers = new();

    public Rover Create(Rover? rover = null)
    {
        rover ??= new Rover();
        _rovers.Add(rover.Id, rover);
        return rover;
    }

    public void Delete(Guid id)
    {
        if (!_rovers.ContainsKey(id))
            throw new NonexistantRoverException($"rover w/ id: {id} does not exist");

        _rovers.Remove(id);
    }

    public Rover Get(Guid id)
        => _rovers.TryGetValue(id, out var rover)
        ? rover : throw new NonexistantRoverException($"rover w/ id: {id} does not exist");

    public IEnumerable<Rover> GetAll() => _rovers.Values.AsEnumerable();

    public Rover Update(Rover rover)
    {
        if(!_rovers.ContainsKey(rover.Id))
            return Create(rover);

        _rovers[rover.Id] = rover;
        return rover;
    }
}
