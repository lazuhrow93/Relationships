using System.Linq;
using Api.Controllers.Dto;
using Api.Controllers.Response;
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
    public async Task<CharacterResponseDto[]> GetUserCharacters(int userId, [FromQuery] bool? includeConnections, CancellationToken cancellationToken)
    {
        var result = await _characters.ForUser(userId, includeConnections ?? false, cancellationToken);

        return result.Select(c =>
        {
            return new CharacterResponseDto()
            {
                UserId = userId,
                CharacterId = c.Id,
                Name = c.Name,
                Connections = c.SourceConnections.Select(sc =>
                {
                    return new CharacterConnectionResponseDto()
                    {
                        CharacterId = sc.TargetCharacter.Id,
                        Name = sc.TargetCharacter.Name,
                        TypeOfConnection = sc.RelationType?.Name?.ToString()
                    };
                }).ToArray()
            };
        }).ToArray();
    }

    [HttpPut]
    [Route("connect")]
    public async Task<ConnectCharactersResponse> ConnectCharacters(ConnectCharactersDto dto, CancellationToken cancellationToken)
    {
        var result = await _connectionService.CreateConnection(
            dto.CharacterFrom,
            dto.CharacterTo,
            dto.ConnectionTypeId,
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
            ConnectionTypeId = result.RelationType?.Id ?? 0
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
                    RoleToCharacter = r.RelationType?.Name ?? "unknown"
                };
            }).ToArray()
        };
    }
    
    [HttpGet]
    [Route("nonconnections/{userId:int}/{characterId:int}")]
    public async Task<GetDisconnectedCharactersResponse> GetNonConnectionsForCharacter(int userId, int characterId, CancellationToken cancellationToken)
    {
        var character = await _characters.Find(characterId, cancellationToken);
        if (character == null)
        {
            throw new Exception("Character not found");
        }

        var disconnectedCharacters = await _characterService.FetchCharactersNotConnectedTo(characterId, userId, cancellationToken);

        return new GetDisconnectedCharactersResponse()
        {
            Disconnections = disconnectedCharacters.Select(c => new DisconnectedCharactersDto()
            {
                CharacterId = c.Id,
                Name = c.Name,
            }).ToArray()
        };
    }
}
