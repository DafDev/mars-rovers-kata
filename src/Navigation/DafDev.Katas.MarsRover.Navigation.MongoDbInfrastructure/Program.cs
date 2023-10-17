using DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson;
using MongoDB.Driver;

const string connectionUri = "mongodb+srv://afahd:yiuipRkKHsVjplQ7@cluster0.bztwrqo.mongodb.net/?retryWrites=true&w=majority";
HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration.AddConfiguration(builder.Environment);

builder.Services.AddServices(builder.Configuration);

using IHost host = builder.Build();

// Application code should start here.

var settings = MongoClientSettings.FromConnectionString(connectionUri);

// Set the ServerApi field of the settings object to Stable API version 1
settings.ServerApi = new ServerApi(ServerApiVersion.V1);

// Create a new client and connect to the server
var client = new MongoClient(settings);

// Send a ping to confirm a successful connection
try
{
    var result = client.GetDatabase("mars").RunCommand<BsonDocument>(new BsonDocument("ping", 1));
    Console.WriteLine("Pinged your deployment. You successfully connected to MongoDB!");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

await host.RunAsync();