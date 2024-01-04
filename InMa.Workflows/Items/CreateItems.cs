using InMa.Abstractions;
using InMa.Contracts.Items;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace InMa.Workflows.Items;

public sealed record CreateItemsCommand(CreateItemRequestModel[]? RequestModel) : IRequest<IResult>;

public sealed class CreateItemsCommandHandler : IRequestHandler<CreateItemsCommand, IResult>
{
    private readonly IItemsService _itemsService;

    public CreateItemsCommandHandler(IItemsService itemsService)
    {
        _itemsService = itemsService;
    }
    
    public async ValueTask<IResult> Handle(CreateItemsCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if ((request.RequestModel?.Length ?? 0) <= 0)
                return Results.NoContent();

            var result = await _itemsService.CreateItems(
                request.RequestModel!.Select(i =>
                    new CreateItemData(Name: i.Name, CategoryName: i.CategoryName)).ToArray(), cancellationToken);

            if (result.IsSuccess)
            {
                return Results.Created("/items",
                    result.GetResult()!.Select(i => new CreateItemResponseModel(
                        Id: i.Id, 
                        Name: i.Name,
                        CategoryName: i.CategoryName, 
                        CreateDate: i.CreateDate)));
            }
            
            return Results.BadRequest(result.GetError());
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}