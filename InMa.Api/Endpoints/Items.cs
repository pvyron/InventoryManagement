using InMa.Contracts.Items;
using InMa.Workflows.Items;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace InMa.Api.Endpoints;

public static class Items
{
    public static async ValueTask<IResult> Get(
        [FromQuery] Guid? id,
        [FromQuery] string? name,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetItemsQuery(id, name), cancellationToken);
    }
    
    public static async ValueTask<IResult> Create(
        [FromBody] CreateItemRequestModel[] createItemRequestModel,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateItemsCommand(createItemRequestModel), cancellationToken);
    }
    
    public static async ValueTask<IResult> Update(
        [FromRoute] Guid id,
        [FromBody] UpdateItemRequestModel createItemRequestModel,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new UpdateItemCommand(id, createItemRequestModel), cancellationToken);
    }
    
    public static async ValueTask<IResult> Delete(
        [FromRoute] Guid id,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new DeleteItemCommand(id), cancellationToken);
    }

    public static async ValueTask<IResult> SearchItems(
        [FromQuery] string? itemName,
        [FromQuery] string? itemCategoryName,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new SearchItemsQuery(itemName, itemCategoryName), cancellationToken);
    }
}