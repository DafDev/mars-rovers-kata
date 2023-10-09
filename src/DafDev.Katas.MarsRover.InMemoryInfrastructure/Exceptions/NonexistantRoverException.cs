namespace DafDev.Katas.MarsRover.InMemoryInfrastructure.Exceptions;
[Serializable]
public class NonexistantRoverException : Exception
{
    public NonexistantRoverException(string? message) : base(message)
    {
    }
}