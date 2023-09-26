using DafDev.Katas.MarsRover.Application.Navigation.Models;

namespace DafDev.Katas.MarsRover.Web.Tests.Data;
public class RoverTestData
{
    public static IEnumerable<object[]> GetForwardCommandData()
    {
        yield return new object[] { CardinalDirections.North, 0, 1 };
        yield return new object[] { CardinalDirections.East, 1, 0 };
        yield return new object[] { CardinalDirections.South, 0, -1 };
        yield return new object[] { CardinalDirections.West, -1, 0 };
    }

    public static IEnumerable<object[]> GetBackwardCommandData()
    {
        yield return new object[] { CardinalDirections.North, 0, -1 };
        yield return new object[] { CardinalDirections.East, -1, 0 };
        yield return new object[] { CardinalDirections.South, 0, 1 };
        yield return new object[] { CardinalDirections.West, 1, 0 };
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
