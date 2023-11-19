using InMa.Contracts.Inventory;
using InMa.Workflows.Inventory;
using Mediator;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace InMa.Api.Endpoints;

public static class Endpoints
{
    public static async ValueTask<IResult> CountStorage(
        [FromBody] CountStorageRequestDto dto,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        var logger = loggerFactory.CreateLogger("");
        
        logger.LogInformation("request");
        return await mediator.Send(new CountStorageRequest(dto), cancellationToken);
    }
    
    public static async ValueTask<IResult> GetInventories(
        [FromQuery(Name = "storageId")] Guid? storageId,
        [FromQuery(Name = "itemId")] Guid? itemId,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        var logger = loggerFactory.CreateLogger("");
        
        logger.LogInformation("request");
        return await mediator.Send(new GetInventories(storageId, itemId), cancellationToken);
    }

    public static async ValueTask<IResult> AddStorageUnit(
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return Results.Created();
    }

    public static async ValueTask<IResult> GetStorageUnits(
        [FromQuery(Name = "storageUnitName")] string? storageUnitName,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return Results.Ok();
    }
}