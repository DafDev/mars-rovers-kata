using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.DependencyInjection;
using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddConfiguration();

builder.Services
    .AddConfiguration(builder.Configuration)
    .AddInfraDependencies();
var mongoSettings = new MongoSettings();
builder.Configuration.GetSection(nameof(MongoSettings)).Bind(mongoSettings);

using IHost host = builder.Build();

// Application code should start here.
var settings = MongoClientSettings.FromConnectionString(mongoSettings?.ConnectionString);

// Set the ServerApi field of the settings object to Stable API version 1
settings.ServerApi = new ServerApi(ServerApiVersion.V1);

// Create a new client and connect to the server
var client = new MongoClient(settings);

// Send a ping to confirm a successful connection
try
{
    var databases = client.ListDatabases().ToList() ;
    var collection = client.GetDatabase("sample_mflix").GetCollection<BsonDocument>("movies");
    var filter = Builders<BsonDocument>.Filter.Eq("title", "Back to the Future");
    var document = collection.Find(filter).First();
    databases.ForEach(Console.WriteLine);
    Console.WriteLine(document);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

await host.RunAsync();