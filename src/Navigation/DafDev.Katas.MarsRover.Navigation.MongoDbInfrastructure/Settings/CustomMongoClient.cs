using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Settings;
public class CustomMongoClient : ICustomMongoClient
{
    private readonly MongoClient _client;
    private readonly ILogger<CustomMongoClient> _logger;

    public CustomMongoClient(IOptionsSnapshot<MongoSettings> mongoSettings, ILogger<CustomMongoClient> logger)
    {
        _logger = logger;
        var settings = MongoClientSettings.FromConnectionString(mongoSettings.Value.ConnectionString);

        // Set the ServerApi field of the settings object to Stable API version 1
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);

        // Create a new client and connect to the server
        _client = new MongoClient(settings);
    }

    public IMongoCollection<TDocument> GetCollectionFromDatabase<TDocument>(string databaseName, string collectionName)
    {
        return _client.GetDatabase(databaseName).GetCollection<TDocument>(collectionName);
    }
}
