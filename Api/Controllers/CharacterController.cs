﻿using Api.Controllers.Dto;
using Api.Dto;
using Data.Queries;
using Domain;
using Domain.Models.Entities;
using Entities;
using Entities.Enums;
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
    public async Task<IActionResult> CreateCharacter([FromBody] CharacterDto dto, CancellationToken cancellationToken)
    {
        var model = new CharacterModel()
        {
            UserId = dto.UserId,
            Name = dto.Name
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
    public async Task<Character[]> GetUserCharacters(int userId, CancellationToken cancellationToken)
    {
        return await _characters.ForUser(userId, cancellationToken);
    }

    [HttpPut]
    [Route("connect")]
    public async Task<ConnectCharactersResponse> ConnectCharacters(ConnectCharactersDto dto, CancellationToken cancellationToken)
    {
        var result = await _connectionService.CreateConnection(
            dto.CharacterFrom,
            dto.CharacterTo,
            dto.ConnectionType,
            cancellationToken);

        if (result == null)
        {
            throw new Exception("Failed to create connection");
        }

        return new ConnectCharactersResponse()
        {
            ConnectionId = result.Id,
            CharacterOneId = result.CharacterOneId,
            CharacterTwoId = result.CharacterTwoId,
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
        var result = await _connections.GetConnectionsForCharacter(characterId, cancellationToken);
        return new GetConnectionsForCharacterResponse()
        {
            CharacterId = characterId,
            ConnectionIds = result.Select(c => c.CharacterTwoId).ToArray()
        };
    }
}
