namespace DafDev.Katas.MarsRover.Navigation.InMemoryInfrastructure.Exceptions;
[Serializable]
public class NonexistantRoverException : Exception
{
    public NonexistantRoverException(string? message) : base(message)
    {
    }
}