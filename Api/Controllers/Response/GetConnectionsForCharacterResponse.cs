using Api.Controllers.Dto;

namespace Api.Controllers.Response;

public class GetConnectionsForCharacterResponse
{
    public int CharacterId { get; set; }
    public GetConnectionsForCharacterDto[] Connections { get; set; } = [];

    public class GetConnectionsForCharacterDto
    {
        public int Id { get; set; }
        public string? CharacterName { get; set; }
        public string? RoleToCharacter { get; set; }
        public int UserId { get; set; }
    }
}
