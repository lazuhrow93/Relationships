using Data;
using Data.Queries;
using Data.Repository;
using Domain;
using Entities;
using Entities.Enums;
using FluentAssertions;
using NSubstitute;
using TestUtility.Faker.Entities;

namespace UnitTests;

public class ConnectionServiceTests
{
    private readonly ConnectionService _sut;

    private readonly ICrudOperator<Connection> _crudOperator;
    private readonly IEntityQueries<Character> _characters;
    private readonly IMyEntity<Connection> _myEntity;

    public ConnectionServiceTests()
    {
        _crudOperator = Substitute.For<ICrudOperator<Connection>>();
        _characters = Substitute.For<IEntityQueries<Character>>();
        _myEntity = Substitute.For<IMyEntity<Connection>>();
        _sut = new(_crudOperator, _characters);
    }

    [Fact]
    public async Task Should_CheckForExisting()
    {
        // Arrange
        _characters
            .FindMany(Arg.Any<HashSet<int>>(), Arg.Any<CancellationToken>())
            .Returns([]);

        // Act
        var result = _sut.CreateConnection(1, 2, ConnectionType.Friend, CancellationToken.None);

        // Assert
        await _characters
            .Received(1)
            .FindMany(Arg.Any<HashSet<int>>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Should_AddConnection()
    {
        // Arrange
        var characters = new CharacterFaker().Generate(2);
        characters[0].Id = 1;
        characters[1].Id = 2;

        _characters
            .FindMany(Arg.Any<HashSet<int>>(), Arg.Any<CancellationToken>())
            .Returns(characters.ToArray());
        _myEntity
            .IsAdded
            .Returns(true);

        // Act
        await _sut.CreateConnection(1, 2, ConnectionType.Friend, CancellationToken.None);

        // Assert
        await _crudOperator
            .Received(1)
            .AddAsync(Arg.Any<Connection>(), CancellationToken.None);
    }

    [Fact]
    public async Task Should_Save()
    {
        // Arrange
        var characters = new CharacterFaker().Generate(2);
        characters[0].Id = 1;
        characters[1].Id = 2;

        _characters
            .FindMany(Arg.Any<HashSet<int>>(), Arg.Any<CancellationToken>())
            .Returns(characters.ToArray());
        _myEntity
            .IsAdded
            .Returns(true);
        _crudOperator
            .AddAsync(Arg.Any<Connection>(), CancellationToken.None)
            .Returns(_myEntity);

        // Act
        await _sut.CreateConnection(1, 2, ConnectionType.Friend, CancellationToken.None);

        // Assert
        await _crudOperator
            .Received(1)
            .SaveChanges(CancellationToken.None);
    }

    [Fact]
    public async Task Should_HaveAllProperties()
    {
        // Arrange
        var characters = new CharacterFaker().Generate(2);
        characters[0].Id = 1;
        characters[1].Id = 2;

        _characters
            .FindMany(Arg.Any<HashSet<int>>(), Arg.Any<CancellationToken>())
            .Returns(characters.ToArray());
        _myEntity
            .IsAdded
            .Returns(true);

        // Act
        var result = await _sut.CreateConnection(1, 2, ConnectionType.Friend, CancellationToken.None);

        // Assert
        result
            .Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(new Connection()
            {
                Id = result!.Id,
                CharacterOneId = 1,
                CharacterTwoId = 2,
                ConnectionType = ConnectionType.Friend,
                CharacterOne = null,
                CharacterTwo = null,
            });
    }
}
