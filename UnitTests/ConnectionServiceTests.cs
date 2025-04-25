using Data;
using Data.Operations;
using Data.Queries;
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

    private readonly ICrudOperator<Connection> _connectionOps;
    private readonly ICrudOperator<ConnectionNote> _connectionNoteOps;
    private readonly ICharacterQueries _characters;
    private readonly IMyEntity<Connection> _myEntity;

    public ConnectionServiceTests()
    {
        _connectionOps = Substitute.For<ICrudOperator<Connection>>();
        _connectionNoteOps = Substitute.For<ICrudOperator<ConnectionNote>>();
        _characters = Substitute.For<ICharacterQueries>();
        _myEntity = Substitute.For<IMyEntity<Connection>>();
        _sut = new(_connectionOps, _connectionNoteOps, _characters);
    }

    [Fact]
    public async Task Should_CheckForExisting()
    {
        // Arrange
        _characters
            .FindMany(Arg.Any<HashSet<int>>(), Arg.Any<CancellationToken>())
            .Returns([]);

        // Act
        var result = _sut.CreateConnection(1, 2, ConnectionType.Friend, null, CancellationToken.None);

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
        await _sut.CreateConnection(1, 2, ConnectionType.Friend, null, CancellationToken.None);

        // Assert
        await _connectionOps
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
        _connectionOps
            .AddAsync(Arg.Any<Connection>(), CancellationToken.None)
            .Returns(_myEntity);

        // Act
        await _sut.CreateConnection(1, 2, ConnectionType.Friend, null, CancellationToken.None);

        // Assert
        await _connectionOps
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
        var result = await _sut.CreateConnection(1, 2, ConnectionType.Friend, null, CancellationToken.None);

        // Assert
        result
            .Should()
            .NotBeNull()
            .And
            .BeEquivalentTo(new Connection()
            {
                Id = result!.Id,
                SourceCharacterId = 1,
                TargetCharacterId = 2,
                ConnectionType = ConnectionType.Friend,
                SourceCharacter = null,
                TargetCharacter = null,
            });
    }
}
