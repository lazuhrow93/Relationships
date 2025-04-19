using Entities.Enums;

namespace Api.Controllers.Dto;

public class ConnectCharactersDto
{
    public int CharacterFrom { get; set; }
    public int CharacterTo { get; set; }
    public ConnectionType ConnectionType { get; set; }
}
