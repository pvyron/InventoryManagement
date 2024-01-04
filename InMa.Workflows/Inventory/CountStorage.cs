using InMa.Contracts.Inventory;
using InMa.DataAccess;
using InMa.DataAccess.Models;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InMa.Workflows.Inventory;

public sealed record CountStorageRequest(CountStorageRequestDto Dto) : IRequest<IResult>;

public sealed class CountStorageHandler(MasterDbContext dbContext) : IRequestHandler<CountStorageRequest, IResult>
{
    public async ValueTask<IResult> Handle(CountStorageRequest request, CancellationToken cancellationToken)
    {
        request.Dto.StorageUnitId ??= dbContext.StorageUnits.Add(new StorageUnit
        {
            Name = request.Dto.StorageUnitName
        }).Entity.Id;

        var storageUnit = await dbContext.StorageUnits.FindAsync([request.Dto.StorageUnitId!], cancellationToken);

        await foreach (var inventory in dbContext.Inventories.Where(i => i.StorageUnitId == storageUnit!.Id)
                           .AsAsyncEnumerable().WithCancellation(cancellationToken))
        {
            inventory.AvailableQuantity = 0;
        }
        
        foreach (var detail in request.Dto.Items)
        {
            detail.ItemId ??= dbContext.Items.Add(new Item
            {
                Name = detail.ItemName,
                CategoryName = detail.ItemCategory
            }).Entity.Id;

            var item = await dbContext.Items.FindAsync([detail.ItemId], cancellationToken);

            var inventory = await dbContext.Inventories.FindAsync([storageUnit!.Id, item!.Id], cancellationToken) ?? dbContext.Inventories.Add(new DataAccess.Models.Inventory
            {
                ItemId = item.Id,
                StorageUnitId = storageUnit.Id
            }).Entity;

            inventory.AvailableQuantity += detail.Quantity;
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        var response = new CountStorageResponseDto
        {
            StorageId = storageUnit!.Id,
            StorageName = storageUnit.Name,
            Items = await dbContext.Inventories.AsNoTracking()
                .Include(i => i.Item)
                .Where(i => i.StorageUnitId == storageUnit.Id)
                .Select(i => new CountStorageResponseItem
                {
                    ItemId = i.Item!.Id,
                    ItemName = i.Item.Name,
                    Quantity = i.AvailableQuantity
                }).ToArrayAsync(cancellationToken)
        };
        
        return Results.Ok(response);
    }
}