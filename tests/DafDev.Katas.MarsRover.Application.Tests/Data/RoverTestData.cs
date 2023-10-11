namespace DafDev.Katas.MarsRover.Navigation.Application.Tests.Data;
public class RoverTestData
{
    public static IEnumerable<object[]> GetLinearMovementCommandData()
    {
        // Forward
        yield return new object[] { CardinalDirections.North, new Coordinates(0, 0), new Coordinates(0, 1), DriverCommands.Forward, "f" };
        yield return new object[] { CardinalDirections.North, new Coordinates(0, int.MinValue), new Coordinates(0, int.MaxValue), DriverCommands.Forward, "f" };
        yield return new object[] { CardinalDirections.East, new Coordinates(0, 0), new Coordinates(1, 0), DriverCommands.Forward, "f" };
        yield return new object[] { CardinalDirections.South, new Coordinates(0, 0), new Coordinates(0, -1), DriverCommands.Forward, "f" };
        yield return new object[] { CardinalDirections.South, new Coordinates(0, int.MaxValue), new Coordinates(0, int.MinValue), DriverCommands.Forward, "f" };
        yield return new object[] { CardinalDirections.West, new Coordinates(0, 0), new Coordinates(-1, 0), DriverCommands.Forward, "f" };

        // Backward
        yield return new object[] { CardinalDirections.North, new Coordinates(0, 0), new Coordinates(0, -1), DriverCommands.Backward, "b" };
        yield return new object[] { CardinalDirections.North, new Coordinates(0, int.MaxValue), new Coordinates(0, int.MinValue), DriverCommands.Backward, "b" };
        yield return new object[] { CardinalDirections.East, new Coordinates(0, 0), new Coordinates(-1, 0), DriverCommands.Backward, "b" };
        yield return new object[] { CardinalDirections.South, new Coordinates(0, 0), new Coordinates(0, 1), DriverCommands.Backward, "b" };
        yield return new object[] { CardinalDirections.South, new Coordinates(0, int.MinValue), new Coordinates(0, int.MaxValue), DriverCommands.Backward, "b" };
        yield return new object[] { CardinalDirections.West, new Coordinates(0, 0), new Coordinates(1, 0), DriverCommands.Backward, "b" };
    }



    public static IEnumerable<object[]> GetTurnCommandData()
    {
        //Right
        yield return new object[] { CardinalDirections.North, CardinalDirections.East, DriverCommands.Right, "r" };
        yield return new object[] { CardinalDirections.East, CardinalDirections.South, DriverCommands.Right, "r" };
        yield return new object[] { CardinalDirections.South, CardinalDirections.West, DriverCommands.Right, "r" };
        yield return new object[] { CardinalDirections.West, CardinalDirections.North, DriverCommands.Right, "r" };

        //Left
        yield return new object[] { CardinalDirections.North, CardinalDirections.West, DriverCommands.Left, "l" };
        yield return new object[] { CardinalDirections.West, CardinalDirections.South, DriverCommands.Left, "l" };
        yield return new object[] { CardinalDirections.South, CardinalDirections.East, DriverCommands.Left, "l" };
        yield return new object[] { CardinalDirections.East, CardinalDirections.North, DriverCommands.Left, "l" };
    }
}
