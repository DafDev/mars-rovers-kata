namespace DafDev.Katas.MarsRover.Navigation.MongoDbInfrastructure.Exceptions;
[Serializable]
public class NonexistantRoverException : Exception
{
    public NonexistantRoverException(string? message) : base(message)
    {
    }
}