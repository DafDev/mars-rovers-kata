namespace DafDev.Katas.MarsRover.Application.Navigation.Exceptions;

[Serializable]
public class UnknownCardinalDirectionException : Exception
{
    public UnknownCardinalDirectionException(string? message) : base(message)
    {
    }
}