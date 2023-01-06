namespace DafDev.Katas.MarsRover.Web.Navigation;
public class DriverTests
{
    private readonly Driver _target;

    public DriverTests()
    {
        _target= new Driver();
    }

    [Theory]
    [MemberData(nameof(GetBackwardCommandData))]
    public void BackwardCommandMovesRoverBackwardBy1UnitFromStartingPosition(char direction, int expectedX, int expectedY)
    {
        //Act
        var result = _target.MoveBackward(new(0, 0), direction);

        //Assert
        Assert.Equal(expectedX, result.X);
        Assert.Equal(expectedY, result.Y);

    }

    public static IEnumerable<object[]> GetBackwardCommandData()
    {
        yield return new object[] { 'N', 0, -1 };
        yield return new object[] { 'E', -1, 0 };
        yield return new object[] { 'S', 0, 1 };
        yield return new object[] { 'W', 1, 0 };
    }
}
