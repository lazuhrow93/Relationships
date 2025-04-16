using Data;
using Data.Repository;
using Domain;
using Entities;
using NSubstitute;
using TestUtility.Faker.Model;
using FluentAssertions;


namespace UnitTests;

public class CharacterServiceTests
{
    private readonly CharacterService _sut;

    private readonly ICrudOperator<Character> _crudOperator;
    private readonly IMyEntity<Character> _myEntity;

    public CharacterServiceTests()
    {
        _crudOperator = Substitute.For<ICrudOperator<Character>>();
        _myEntity = Substitute.For<IMyEntity<Character>>();
        _sut = new CharacterService(_crudOperator);
    }

    [Fact]
    public async Task Should_Add()
    {
        //arrange
        var model = new CharacterModelFaker().Generate();
        _myEntity
            .IsAdded
            .Returns(true);
        _crudOperator
            .AddAsync(Arg.Any<Character>(), CancellationToken.None)
            .Returns(_myEntity);

        //act
        var result = await _sut.CreateCharacter(model, CancellationToken.None);

        //assert
        await _crudOperator
            .Received(1)
            .AddAsync(Arg.Is<Character>(x => x.UserId == model.UserId && x.Name == model.Name), CancellationToken.None);
    }

    [Fact]
    public async Task Should_Save()
    {
        //arrange
        var model = new CharacterModelFaker().Generate();
        _myEntity
            .IsAdded
            .Returns(true);
        _crudOperator
            .AddAsync(Arg.Any<Character>(), CancellationToken.None)
            .Returns(_myEntity);

        //act
        var result = await _sut.CreateCharacter(model, CancellationToken.None);

        //assert
        await _crudOperator
            .Received(1)
            .SaveChanges(CancellationToken.None);
    }

    [Fact]
    public async Task Should_NotSave()
    {
        //arrange
        var model = new CharacterModelFaker().Generate();
        _myEntity
            .IsAdded
            .Returns(false);
        _crudOperator
            .AddAsync(Arg.Any<Character>(), CancellationToken.None)
            .Returns(_myEntity);

        //act
        var result = await _sut.CreateCharacter(model, CancellationToken.None);

        //assert
        await _crudOperator
            .DidNotReceive()
            .SaveChanges(CancellationToken.None);
    }

    [Fact]
    public async Task Should_ReturnCharacter()
    {
        //arrange
        var model = new CharacterModelFaker().Generate();
        _myEntity
            .IsAdded
            .Returns(true);
        _crudOperator
            .AddAsync(Arg.Any<Character>(), CancellationToken.None)
            .Returns(_myEntity);

        //act
        var result = await _sut.CreateCharacter(model, CancellationToken.None);

        //assert
        result.Should()
            .NotBeNull();
    }

    [Fact]
    public async Task Should_MatchProperties()
    {
        //arrange
        var model = new CharacterModelFaker().Generate();
        _myEntity
            .IsAdded
            .Returns(true);
        _crudOperator
            .AddAsync(Arg.Any<Character>(), CancellationToken.None)
            .Returns(_myEntity);

        //act
        var result = await _sut.CreateCharacter(model, CancellationToken.None);

        //assert
        result.Should()
            .BeEquivalentTo(new Character()
            {
                Id = result.Id,
                Name = model.Name,
                UserId = model.UserId
            });
    }
}