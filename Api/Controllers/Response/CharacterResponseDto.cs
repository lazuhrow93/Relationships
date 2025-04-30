using Entities;

namespace Api.Controllers.Response;

public class CharacterResponseDto
{
    public int UserId { get; set; }

    public int CharacterId { get; set; }

    public string? Name { get; set; }

    public CharacterConnectionResponseDto[] Connections { get; set; } = [];
}
