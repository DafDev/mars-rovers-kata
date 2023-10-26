using MongoDB.Driver;

namespace DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Settings;
public interface ICustomMongoClient
{
    IMongoCollection<TDocument> GetCollectionFromDatabase<TDocument>(string databaseName, string collectionName);    
}
