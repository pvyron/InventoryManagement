using InMa.Abstractions;
using InMa.Core.Types;
using InMa.DataAccess;
using InMa.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InMa.Logic;

public sealed class ItemsService : IItemsService
{
    private readonly MasterDbContext _dbContext;

    public ItemsService(MasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask<Result<List<CreatedItemData>>> CreateItems(CreateItemData[] createItemsData, CancellationToken cancellationToken)
    {
        if (createItemsData.GroupBy(i => i.Name).Any(ig => ig.Count() > 1))
            return (Result<List<CreatedItemData>>)"One or more items have the same name!";
        
        var createdItems = new List<CreatedItemData>();

        foreach (var itemRequestModel in createItemsData)
        {
            if (await _dbContext.Items.AsNoTracking().AnyAsync(i => i.Name == itemRequestModel.Name, cancellationToken))
                return new Result<List<CreatedItemData>>($"Item with name {itemRequestModel.Name} already exists!");

            var createdItem = _dbContext.Items.Add(new Item
            {
                Id = Guid.NewGuid(),
                Name = itemRequestModel.Name,
                CategoryName = itemRequestModel.CategoryName,
            }).Entity;

            createdItems.Add(new CreatedItemData(
                Id: createdItem.Id,
                Name: createdItem.Name,
                CategoryName: createdItem.CategoryName,
                CreateDate: createdItem.CreateDate));
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new Result<List<CreatedItemData>>(createdItems);
    }
}