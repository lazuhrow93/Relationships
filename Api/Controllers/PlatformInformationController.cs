﻿using Api.Controllers.PlatformInformation;
using Data.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PlatformInformationController : Controller
{
    private readonly IRelationTypeQueries _relationTypeQueries;

    public PlatformInformationController(IRelationTypeQueries relationTypeQueries)
    {
        _relationTypeQueries = relationTypeQueries;
    }

    [HttpGet]
    [Route("relation-types")]
    public async Task<RelationTypesResponse> GetRelationTypes(CancellationToken cancellationToken)
    {
        return new RelationTypesResponse()
        {
            Types = (await _relationTypeQueries.GetAll(cancellationToken))
                .Select(r => new RelationTypesResponse.RelationTypesDto()
                {
                    RelationTypeId = r.Id,
                    Name = r.Name
                })
                .ToArray()
        };
    }
}
