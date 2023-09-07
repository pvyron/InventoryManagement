using InMa.Contracts.Inventory;
using InMa.DataAccess;
using InMa.DataAccess.Models;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace InMa.Workflows.Inventory;

public sealed record PostInventoryRequest(PostInventoryDto Dto) : IRequest<IResult>;

public sealed class PostInventoryHandler : IRequestHandler<PostInventoryRequest, IResult>
{
    private readonly MasterDbContext _masterDb;

    public PostInventoryHandler(MasterDbContext masterDb)
    {
        _masterDb = masterDb;
    }
    
    public async ValueTask<IResult> Handle(PostInventoryRequest request, CancellationToken cancellationToken)
    {
        foreach (var detail in request.Dto.Details)
        {
            if (detail.ItemId is null)
            {
                _masterDb.Items.Add(new Item
                {
                    ItemName = detail.ItemName,
                    ItemCategory = detail.ItemCategory
                });
            }
        }

        await _masterDb.SaveChangesAsync(cancellationToken);
        
        return Results.Ok(request.Dto);
    }
}