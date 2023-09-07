using InMa.Contracts.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace InMa.Api.Endpoints;

public static class Endpoints
{
    public static async ValueTask<IResult> PostInventory(
        [FromBody] PostInventoryDto dto,
        [FromServices] ILoggerFactory loggerFactory)
    {
        var logger = loggerFactory.CreateLogger("");
        
        logger.LogInformation("request");
        return Results.Ok(dto);
    }
}