namespace Api.Controllers.PlatformInformation;

public class RelationTypesResponse
{
    public RelationTypesDto[] Types { get; set; } = [];

    public class RelationTypesDto
    {
        public int RelationTypeId { get; set; }
        public string? Name { get; set; }
    }
}
