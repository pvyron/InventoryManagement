using InMa.DataAccess;
using InMa.DataAccess.Models;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InMa.Workflows.Inventory;

public sealed record GetInventories(Guid? Storageid, Guid? Itemid): IRequest<IResult>;

public sealed class GetInventoriesHandler(MasterDbContext dbContext) : IRequestHandler<GetInventories, IResult>
{
    public async ValueTask<IResult> Handle(GetInventories request, CancellationToken cancellationToken)
    {
        var inventoriesQuery = dbContext.Inventories
            .Include(i => i.StorageUnit)
            .Include(i => i.Item)
            .AsNoTracking();

        if (request.Storageid is not null)
            inventoriesQuery = inventoriesQuery.Where(i => i.StorageUnitId == request.Storageid);

        if (request.Itemid is not null)
            inventoriesQuery = inventoriesQuery.Where(i => i.ItemId == request.Itemid);

        var inventories = await inventoriesQuery.Select(i => new
        {
            i.StorageUnitId,
            StorageName = i.StorageUnit!.Name,
            i.ItemId,
            ItemName = i.Item!.Name,
            i.AvailableQuantity,
            i.MaxQuantity
        }).ToListAsync(cancellationToken);
        
        return Results.Ok(inventories);
    }
}