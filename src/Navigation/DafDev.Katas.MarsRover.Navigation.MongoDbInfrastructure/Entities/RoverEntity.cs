using MongoDB.Bson.Serialization.Attributes;

namespace DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Entities;

public class RoverEntity
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    public CoordinatesEntity Position { get; set; }
    public CardinalDirectionsEntity Direction { get; set; }
    public Guid RoverId { get; init; }

    public RoverEntity()
    {
        Position = new CoordinatesEntity(0, 0);
        Direction = CardinalDirectionsEntity.North;
        RoverId = Guid.NewGuid();
    }
}
