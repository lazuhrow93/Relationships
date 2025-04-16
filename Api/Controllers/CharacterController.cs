using Api.Dto;
using Domain;
using Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
public class CharacterController : Controller
{
    public readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;    
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
}
