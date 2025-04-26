using Api.Controllers.Dto;
using Data.Queries;
using Domain;
using Domain.Models.Entities;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class CharacterController : Controller
{
    private readonly ICharacterService _characterService;
    private readonly IConnectionService _connectionService;

    private readonly ICharacterQueries _characters;
    private readonly IConnectionQueries _connections;

    public CharacterController(ICharacterService characterService,
        ICharacterQueries characters,
        IConnectionService connectionService,
        IConnectionQueries connectionQueries)
    {
        _characterService = characterService;
        _characters = characters;
        _connectionService = connectionService;
        _connections = connectionQueries;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCharacter([FromBody] ConnectionDescriptionDto dto, CancellationToken cancellationToken)
    {
        var model = new CharacterModel()
        {
            UserId = dto.UserId,
            Name = dto.CharacterName
        };

        var result = await _characterService.CreateCharacter(model, cancellationToken);
        if (result == null)
        {
            return BadRequest("Failed to create character");
        }
        return CreatedAtAction(nameof(CreateCharacter), new { id = result.Id }, result);
    }

    [HttpGet]
    [Route("{userId:int}")]
    public async Task<Character[]> GetUserCharacters(int userId, [FromQuery] bool? includeConnections, CancellationToken cancellationToken)
    {
        return await _characters.ForUser(userId, includeConnections ?? false, cancellationToken);
    }

    [HttpPut]
    [Route("connect")]
    public async Task<ConnectCharactersResponse> ConnectCharacters(ConnectCharactersDto dto, CancellationToken cancellationToken)
    {
        var result = await _connectionService.CreateConnection(
            dto.CharacterFrom,
            dto.CharacterTo,
            dto.ConnectionType,
            dto.Note,
            cancellationToken);

        if (result == null)
        {
            throw new Exception("Failed to create connection");
        }

        return new ConnectCharactersResponse()
        {
            ConnectionId = result.Id,
            CharacterOneId = result.SourceCharacterId,
            CharacterTwoId = result.TargetCharacterId,
            ConnectionTypeId = (int)result.ConnectionType
        };
    }

    [HttpGet]
    [Route("connections/{characterId:int}")]
    public async Task<GetConnectionsForCharacterResponse> GetConnectionsForCharacter(int characterId, CancellationToken cancellationToken)
    {
        var character = await _characters.Find(characterId, cancellationToken);
        if (character == null)
        {
            throw new Exception("Character not found");
        }
        
        var result = await _connections.GetConnectionsForCharacter(characterId, CharacterQueryOptions.IncludeCharacters, cancellationToken);
        return new GetConnectionsForCharacterResponse()
        {
            CharacterId = characterId,
            Connections = result.Select(r =>
            {
                return new ConnectionDescriptionDto()
                {
                    Id = r.TargetCharacter.Id,
                    CharacterName = r.TargetCharacter.Name,
                    RoleToCharacter = r.ConnectionType.ToString()
                };
            }).ToArray()
        };
    }
}
