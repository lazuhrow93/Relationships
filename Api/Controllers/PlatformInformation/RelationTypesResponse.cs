namespace Api.Controllers.PlatformInformation;

public class RelationTypesResponse
{
    public Dto[] Types { get; set; } = [];

    public class Dto
    {
        public int RelationTypeId { get; set; }
        public string? Name { get; set; }
    }
}
