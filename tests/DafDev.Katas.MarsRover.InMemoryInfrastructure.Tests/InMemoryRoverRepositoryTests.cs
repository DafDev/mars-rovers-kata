using DafDev.Katas.MarsRover.Navigation.Domain.Models;
using DafDev.Katas.MarsRover.Navigation.InMemoryInfrastructure.Repository;
using DafDev.Katas.MarsRover.Navigation.InMemoryInfrastructure.Exceptions;
using Xunit;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace DafDev.Katas.MarsRover.Navigation.InMemoryInfrastructure.Tests;

public class InMemoryRoverRepositoryTests
{
    public readonly InMemoryRoverRepository _sut = new(Substitute.For<ILogger<InMemoryRoverRepository>>());

    [Fact]
    public async Task CreateShouldReturnGuid()
    {
        // Arrange
        var rover = new Rover();

        // Act
        var result = await _sut.Create(rover);

        // Assert
        result.Should().Be(rover);
    }
    [Fact]
    public async Task CreatefromNullRoverShouldReturnGuid()
    {
        // Arrange & Act
        var result = await _sut.Create();

        // Assert
        result.Should().NotBeNull();
    }
    [Fact]
    public async Task UpdateUnknownRoverShouldCreateIt()
    {
        // Arrange
        var rover = new Rover();

        // Act
        var result = await _sut.Update(rover);

        // Assert
        result.Should().Be(rover);
    }

    [Fact]
    public async Task UpdateKnownRoverShouldupdateIt()
    {
        // Arrange
        var rover = new Rover();
        var created = await _sut.Create(rover);
        created.Should().Be(rover);
        var modified = new Rover
        {
            RoverId = created.RoverId,
            Direction = CardinalDirections.West,
            Position = new Coordinates(-26, 87),
        };

        // Act
        var result = await _sut.Update(modified);

        // Assert
        result.Should().Be(modified);
    }

    [Fact]
    public async Task GetAllShouldReturnAllRoversInMemory()
    {
        // Arrange
        var rover = new Rover();
        await _sut.Create(rover);

        // Act
        var result = await _sut.GetAll();

        // Assert
        result.Should().BeEquivalentTo(new List<Rover> { rover });
    }


    [Fact]
    public async Task GetByIdShouldReturnAppropriateRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        await _sut.Create(rover);

        // Act
        var result = await _sut.Get(rover.RoverId);

        // Assert
        result.Should().Be(rover);
    }

    [Fact]
    public async Task DeleteShouldDeleteRover()
    {
        // Arrange
        var rover = new Rover();
        await _sut.Create(rover);
        var gottenRover = await _sut.Get(rover.RoverId);
        gottenRover.Should().Be(rover);

        // Act
        await _sut.Delete(rover.RoverId);

        // Assert
        var result = () => _sut.Get(rover.RoverId);
        await result.Should().ThrowAsync<NonexistantRoverException>();
    }

    [Fact]
    public async Task DeleteIfGuidUnknownShouldThrow()
    {
        // Arrange & Act
        var action = () => _sut.Delete(new Guid());

        // Assert
        await action.Should().ThrowAsync<NonexistantRoverException>();
    }
}