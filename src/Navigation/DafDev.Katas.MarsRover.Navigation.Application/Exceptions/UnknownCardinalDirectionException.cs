namespace DafDev.Katas.MarsRover.Navigation.Application.Exceptions;

[Serializable]
public class UnknownCardinalDirectionException : Exception
{
    public UnknownCardinalDirectionException(string? message) : base(message)
    {
    }
}