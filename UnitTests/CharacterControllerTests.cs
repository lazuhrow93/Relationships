using Api.Controllers;
using Data.Queries;
using Domain;
using FluentAssertions;
using NSubstitute;
using TestUtility.Faker.Entities;

namespace UnitTests;

public class CharacterControllerTests
{
    private readonly ICharacterService _characterService = Substitute.For<ICharacterService>();
    private readonly ICharacterQueries _characterQueries = Substitute.For<ICharacterQueries>();
    private readonly IConnectionService _connectionService = Substitute.For<IConnectionService>();
    private readonly IConnectionQueries _connectionQueries = Substitute.For<IConnectionQueries>();
    private readonly CharacterController _controller;

    public CharacterControllerTests()
    {
        _controller = new CharacterController(_characterService, _characterQueries, _connectionService, _connectionQueries);
    }

    [Fact]
    public async Task GetUserCharacters_Should_ReturnCharacters()
    {
        // Arrange
        var userId = 1;
        var characters = new CharacterFaker().Generate(2).ToArray();

        _characterQueries
            .ForUser(userId, Arg.Any<bool>(), Arg.Any<CancellationToken>())
            .Returns(characters);

        // Act
        var result = await _controller.GetUserCharacters(userId, null, CancellationToken.None);

        // Assert
        result
            .Should()
            .HaveCount(2);
    }

    [Fact]
    public async Task GetUserCharacters_WithConnection()
    {
        // Arrange
        var characterFaker = new CharacterFaker();
        var connectionFaker = new ConnectionFaker();
        var character = characterFaker.Generate();
        var connection = connectionFaker.Generate();
        character.TargetConnections = [connection];

        _characterQueries
            .ForUser(1, true, Arg.Any<CancellationToken>())
            .Returns([character]);

        // Act
        var result = await _controller.GetUserCharacters(1, true, CancellationToken.None);

        // Assert
        result
            .Should()
            .HaveCount(1);
        result[0]
            .TargetConnections.Should()
            .NotBeEmpty();
    }
}
