namespace DafDev.Katas.MarsRover.Navigation.Application.Exceptions;
[Serializable]
public class UnknownDriverCommandException : Exception
{
    public UnknownDriverCommandException(string? message) : base(message)
    {
    }
}