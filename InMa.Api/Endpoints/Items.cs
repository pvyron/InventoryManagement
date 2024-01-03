using InMa.Contracts.Items;
using InMa.Workflows.Items;
using Mediator;
using Microsoft.AspNetCore.Mvc;

namespace InMa.Api.Endpoints;

public static class Items
{
    public static async ValueTask<IResult> Create(
        [FromBody] CreateItemRequestModel[] createItemRequestModel,
        [FromServices] IMediator mediator,
        [FromServices] ILoggerFactory loggerFactory,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new CreateItem(createItemRequestModel), cancellationToken);
    }
}