using InMa.Contracts.Inventory;
using InMa.Workflows.Inventory;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace InMa.Api.Endpoints;

public static class Endpoints
{
    public static async ValueTask<IResult> PostInventory(
        [FromBody] PostInventoryDto dto,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        var logger = loggerFactory.CreateLogger("");
        
        logger.LogInformation("request");
        return await mediator.Send(new PostInventoryRequest(dto), cancellationToken);
    }
}