using AutoMapper;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using DafDev.Katas.MarsRover.Navigation.Domain.Repository;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Entities;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Exceptions;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Settings;
using Microsoft.Extensions.Logging;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

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

    public async Task Delete(Guid roverId)
    {
        var deletResult = await _rovers.DeleteOneAsync(entity => entity.RoverId == roverId);
        if (!deletResult.IsAcknowledged)
            LogAndThrowException($"rover w/ roverId: {deletResult} does not exist");
    }

    public async Task<Rover> Get(Guid id)
        => _mapper.Map<Rover>(await _rovers.FindAsync(rover => rover.RoverId == id).Result.FirstOrDefaultAsync());

    public async Task<IEnumerable<Rover>> GetAll()
    {
        var allRovers = new List<Rover>();
        await _rovers.FindAsync(_ => true).Result.ForEachAsync(roverEntity => allRovers.Add(_mapper.Map<Rover>(roverEntity)));
        return allRovers;
    }

    public async Task<Rover> UpdateOrCreate(Rover rover)
    {
        var update = Builders<RoverEntity>
            .Update
            .Set(entity => entity.Position, _mapper.Map<CoordinatesEntity>(rover.Position))
            .Set(entity => entity.Direction, _mapper.Map<CardinalDirectionsEntity>(rover.Direction));

        var options = new FindOneAndUpdateOptions<RoverEntity>()
        {
            IsUpsert = true,
            ReturnDocument = ReturnDocument.After
        };

        var updatedRover = await _rovers.FindOneAndUpdateAsync<RoverEntity>(roverEntity => roverEntity.RoverId == rover.RoverId,
            update,
            options);

        return _mapper.Map<Rover>(updatedRover);
    }

    private Rover LogAndThrowException(string message)
    {
        _logger.LogError(message);
        throw new NonexistantRoverException(message);
    }
}
