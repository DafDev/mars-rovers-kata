using DafDev.Katas.MarsRover.Application.Navigation.Models;
using DafDev.Katas.MarsRover.InMemoryInfrastructure.Repository;
using DafDev.Katas.MarsRover.InMemoryInfrastructure.Exceptions;
using Xunit;
using FluentAssertions;

namespace DafDev.Katas.MarsRover.InMemoryInfrastructure.Tests;

public class InMemoryRoverRepositoryTests
{
    public readonly InMemoryRoverRepository _sut = new();

    [Fact]
    public void CreateShouldReturnGuid()
    {
        // Arrange
        var rover = new Rover();

        // Act
        var result =_sut.Create(rover);

        // Assert
        result.Should().Be(rover);
    }

    [Fact]
    public void UpdateUnknownRoverShouldCreateIt()
    {
        // Arrange
        var rover = new Rover();

        // Act
        var result = _sut.Update(rover);

        // Assert
        result.Should().Be(rover);
    }

    [Fact]
    public void UpdateKnownRoverShouldupdateIt()
    {
        // Arrange
        var rover = new Rover();
        var created = _sut.Create(rover);
        created.Should().Be(rover);
        var modified = new Rover
        {
            Id = created.Id,
            Direction = CardinalDirections.West,
            Position = new Coordinates(-26, 87),
        };

        // Act
        var result = _sut.Update(modified);

        // Assert
        result.Should().Be(modified);
    }

    [Fact]
    public void GetAllShouldReturnAllRoversInMemory()
    {
        // Arrange
        var rover = new Rover();
        _sut.Create(rover);

        // Act
        var result = _sut.GetAll();

        // Assert
        result.Should().BeEquivalentTo(new List<Rover> { rover });
    }


    [Fact]
    public void GetByIdShouldReturnAppropriateRoverInMemory()
    {
        // Arrange
        var rover = new Rover();
        _sut.Create(rover);

        // Act
        var result = _sut.Get(rover.Id);

        // Assert
        result.Should().Be(rover);
    }

    [Fact]
    public void DeleteShouldDeleteRover()
    {
        // Arrange
        var rover = new Rover();
        _sut.Create(rover);
        var gottenRover = _sut.Get(rover.Id);
        gottenRover.Should().Be(rover);

        // Act
        _sut.Delete(rover.Id);

        // Assert
        var result = () => _sut.Get(rover.Id);
        result.Should().Throw<NonexistantRoverException>();
    }

    [Fact]
    public void DeleteIfGuidUnknownShouldThrow()
    {
        // Arrange & Act
        var action = () => _sut.Delete(new Guid());

        // Assert
        action.Should().Throw<NonexistantRoverException>();
    }
}