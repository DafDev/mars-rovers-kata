using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using DafDev.Katas.MarsRover.Navigation.Domain.Repository;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Exceptions;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Repository;
public class MongoRepository : IRoverRepository
{
    //private readonly IMongoCollection<Rover> _rovers;
    private readonly Dictionary<Guid, Rover> _rovers = new();
    private readonly ILogger<MongoRepository> _logger;

    public MongoRepository(ILogger<MongoRepository> logger)
    {
        _logger = logger;

        // This allows automapping of the camelCase database fields to our models. 
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        // Establish the connection to MongoDB and get the restaurants database
        //var mongoClient = new MongoClient(MongoConnectionString);
        //var restaurantsDatabase = mongoClient.GetDatabase("sample_restaurants");
        //_restaurantsCollection = restaurantsDatabase.GetCollection<Restaurant>("restaurants");
    }

    public async Task<Rover> Create(Rover? rover = null)
    {
        rover ??= new Rover();
        //await _rovers.InsertOneAsync(rover);
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

    public async Task<Rover> Update(Rover rover)
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
