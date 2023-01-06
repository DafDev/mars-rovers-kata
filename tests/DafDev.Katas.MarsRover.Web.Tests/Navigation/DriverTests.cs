namespace DafDev.Katas.MarsRover.Web.Navigation;
public class DriverTests
{
    private readonly Driver _target;

    public DriverTests()
    {
        _target= new Driver();
    }

    [Theory]
    [MemberData(nameof(GetForwardCommandData))]
    public void MoveForwardMovesRoverForwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        //Act
        var result = _target.MoveForward(new(0, 0), direction);

        //Assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);

    }

    [Theory]
    [MemberData(nameof(GetBackwardCommandData))]
    public void MoveBackwardMovesRoverBackwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        //Act
        var result = _target.MoveBackward(new(0, 0), direction);

        //Assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);

    }

    [Theory]
    [InlineData('N','E')]
    public void TurnRightMovesTheDirectionClockwise(char startingDirection, char expectedDirection)
    {
        //Act
        char result = _target.TurnRight(startingDirection);

        //Assert
        Assert.Equal(expectedDirection, result);
    }

    public static IEnumerable<object[]> GetBackwardCommandData()
    {
        yield return new object[] { 'N', 0, -1 };
        yield return new object[] { 'E', -1, 0 };
        yield return new object[] { 'S', 0, 1 };
        yield return new object[] { 'W', 1, 0 };
    }

    public static IEnumerable<object[]> GetForwardCommandData()
    {
        yield return new object[] { 'N', 0, 1 };
        yield return new object[] { 'E', 1, 0 };
        yield return new object[] { 'S', 0, -1 };
        yield return new object[] { 'W', -1, 0 };
    }
}
