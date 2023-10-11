namespace DafDev.Katas.MarsRover.Navigation.Domain.Exceptions;

[Serializable]
public class UnknownCardinalDirectionException : Exception
{
    public UnknownCardinalDirectionException(string? message) : base(message)
    {
    }
}