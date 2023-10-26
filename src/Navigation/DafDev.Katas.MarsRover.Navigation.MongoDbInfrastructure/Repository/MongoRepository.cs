using AutoMapper;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using DafDev.Katas.MarsRover.Navigation.Domain.Repository;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Exceptions;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Settings;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Entities;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System.Linq;

namespace DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Repository;
public class MongoRepository : IRoverRepository
{
    private const string RoversMongoCollectionName = "rovers";
    private const string MarsMongoDatabaseName = "mars";
    private readonly IMongoCollection<RoverEntity> _rovers;
    private readonly IMapper _mapper;
    private readonly ILogger<MongoRepository> _logger;


    public MongoRepository(ICustomMongoClient client, ILogger<MongoRepository> logger, IMapper mapper)
    {
        _logger = logger;

        // This allows automapping of the camelCase database fields to our models. 
        var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("CamelCase", camelCaseConvention, type => true);

        _rovers = client.GetCollectionFromDatabase<RoverEntity>(MarsMongoDatabaseName, RoversMongoCollectionName);
        _mapper = mapper;
    }

    public async Task<Rover> Create(Rover? rover = null)
    {
        rover ??= new Rover();
        await _rovers.InsertOneAsync(_mapper.Map<RoverEntity>(rover));
        return rover;
    }

    public async Task Delete(Guid id)
    {
        //var result =
        //if (!await Task.FromResult(_rovers.Remove(id)))
            //LogAndThrowException($"rover w/ id: {id} does not exist");
    }

    public async Task<Rover> Get(Guid id)
        => _mapper.Map<Rover>(await _rovers.FindAsync(rover => rover.RoverId == id).Result.FirstOrDefaultAsync());

    public async Task<IEnumerable<Rover>> GetAll()
    {
        var allRovers = new List<Rover>();
        await _rovers.FindAsync(_ => true).Result.ForEachAsync(roverEntity => allRovers.Add(_mapper.Map<Rover>(roverEntity)));
        return allRovers;
    }

    public async Task<Rover> Update(Rover rover)
    {
        //if(!_rovers.ContainsKey(rover.RoverId))
        //    return await Create(rover);

        //_rovers[rover.RoverId] = rover;
        return rover;
    }

    private Rover LogAndThrowException(string message)
    {
        _logger.LogError(message);
        throw new NonexistantRoverException(message);
    }
}
