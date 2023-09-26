namespace DafDev.Katas.MarsRover.Application.Navigation.Exceptions;
[Serializable]
public class UnknownDriverCommandException : Exception
{
    public UnknownDriverCommandException(string? message) : base(message)
    {
    }
}