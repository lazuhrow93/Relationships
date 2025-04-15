using Data;
using Data.Repository;
using Domain;
using Entities;
using NSubstitute;
using TestUtility.Faker.Model;

namespace UnitTests;

public class CharacterServiceTests
{
    private readonly CharacterService _sut;

    private readonly ICrudOperator<Character> _crudOperator;

    public CharacterServiceTests()
    {
        _crudOperator = Substitute.For<ICrudOperator<Character>>();
        _sut = new CharacterService(_crudOperator);
    }

    [Fact]
    public async Task Should_Add()
    {
        //arrange
        var model = new CharacterModelFaker().Generate();
        _crudOperator
            .AddAsync(Arg.Any<Character>(), CancellationToken.None)
            .Returns(new MyEntity<Character>(new Character() { UserId = model.UserId, Name = model.Name }));

        //act
        var result = await _sut.CreateCharacter(model, CancellationToken.None);

        //assert
        await _crudOperator
            .Received(1)
            .AddAsync(Arg.Is<Character>(x => x.UserId == model.UserId && x.Name == model.Name), CancellationToken.None);
    }
}