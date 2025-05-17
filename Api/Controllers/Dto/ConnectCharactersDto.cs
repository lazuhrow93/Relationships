namespace Api.Controllers.Dto;

public class ConnectCharactersDto
{
    public int CharacterFrom { get; set; }
    public int CharacterTo { get; set; }
    public string? Note { get; set; }
    public int ConnectionTypeId { get; set; }
}
