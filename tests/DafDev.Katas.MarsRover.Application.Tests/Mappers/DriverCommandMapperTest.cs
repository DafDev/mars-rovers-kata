using DafDev.Katas.MarsRover.Navigation.Application.Mappers;
using DafDev.Katas.MarsRover.Navigation.Application.Exceptions;
using DafDev.Katas.MarsRover.Navigation.Domain.Models;

namespace DafDev.Katas.MarsRover.Navigation.Application.Tests.Mappers;
public class DriverCommandMapperTest
{
    private readonly DriverCommandMapper _sut = new();

    [Theory]
    [InlineData("fbrl")]
    [InlineData("FBRL")]
    public void DriverMapperMapsCorrectly(string commands)
    {
        //Arrange
        var expectedMappedCommands = new List<DriverCommands>
        {
            DriverCommands.Forward,
            DriverCommands.Backward,
            DriverCommands.Right,
            DriverCommands.Left
        };

        //Act
        var actual = _sut.Map(commands);

        //Assert
        actual.Should().BeEquivalentTo(expectedMappedCommands);
    }

    [Fact]
    public void DriverMapperThrowsWhenUnknownComand()
    {
        // Arrange & Act
        var action = () => _sut.Map('a');

        // Assert
        action
            .Should()
            .Throw<UnknownDriverCommandException>()
            .WithMessage("'a' command does not exist");
    }
}

