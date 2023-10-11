using Castle.Core.Logging;
using DafDev.Katas.MarsRover.Navigation.Application.Exceptions;
using DafDev.Katas.MarsRover.Navigation.Application.Mappers;
using Microsoft.Extensions.Logging;
using Moq;

namespace DafDev.Katas.MarsRover.Navigation.Application.Tests.Mappers;
public class DriverCommandMapperTest
{
    private readonly Mock<ILogger<DriverCommandMapper>> _logger= new();
    private readonly DriverCommandMapper _sut;

    public DriverCommandMapperTest() => _sut = new(_logger.Object);

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
        _logger.Verify(logger => logger.Log(
                It.Is<LogLevel>(logLevel => logLevel == LogLevel.Error),
                It.Is<EventId>(eventId => eventId == 0),
                It.Is<It.IsAnyType>((@object, @type) => @object.ToString() == "'a' command does not exist" && @type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once);
    }
}

