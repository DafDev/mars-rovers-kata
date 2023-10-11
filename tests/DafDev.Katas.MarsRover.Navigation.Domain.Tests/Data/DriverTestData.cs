using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Domain.Tests.Data;
public class DriverTestData
{
    public static IEnumerable<object[]> GetForwardCommandData()
    {
        yield return new object[] { CardinalDirections.North, new Coordinates(0, 0), new Coordinates(0, 1) };
        yield return new object[] { CardinalDirections.North, new Coordinates(0, int.MinValue), new Coordinates(0, int.MaxValue) };
        yield return new object[] { CardinalDirections.East, new Coordinates(0, 0), new Coordinates(1, 0) };
        yield return new object[] { CardinalDirections.South, new Coordinates(0, 0), new Coordinates(0, -1) };
        yield return new object[] { CardinalDirections.South, new Coordinates(0, int.MaxValue), new Coordinates(0, int.MinValue) };
        yield return new object[] { CardinalDirections.West, new Coordinates(0, 0), new Coordinates(-1, 0) };
        
    }

    public static IEnumerable<object[]> GetBackwardCommandData()
    {
        yield return new object[] { CardinalDirections.North, new Coordinates(0, 0), new Coordinates(0, -1) };
        yield return new object[] { CardinalDirections.North, new Coordinates(0, int.MaxValue), new Coordinates(0, int.MinValue) };
        yield return new object[] { CardinalDirections.East, new Coordinates(0, 0), new Coordinates(-1, 0) };
        yield return new object[] { CardinalDirections.South, new Coordinates(0, 0), new Coordinates(0, 1) };
        yield return new object[] { CardinalDirections.South, new Coordinates(0, int.MinValue), new Coordinates(0, int.MaxValue) };
        yield return new object[] { CardinalDirections.West, new Coordinates(0, 0), new Coordinates(1, 0) };
    }

    public static IEnumerable<object[]> GetTurnRightCommandData()
    {
        yield return new object[] { CardinalDirections.North, CardinalDirections.East };
        yield return new object[] { CardinalDirections.East, CardinalDirections.South };
        yield return new object[] { CardinalDirections.South, CardinalDirections.West };
        yield return new object[] { CardinalDirections.West, CardinalDirections.North };
    }

    public static IEnumerable<object[]> GetTurnLeftCommandData()
    {
        yield return new object[] { CardinalDirections.North, CardinalDirections.West };
        yield return new object[] { CardinalDirections.West, CardinalDirections.South };
        yield return new object[] { CardinalDirections.South, CardinalDirections.East };
        yield return new object[] { CardinalDirections.East, CardinalDirections.North };
    }
}
