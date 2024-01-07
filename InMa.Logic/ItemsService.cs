using InMa.Abstractions;
using InMa.Core.Types;
using InMa.DataAccess;
using InMa.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InMa.Logic;

public sealed class ItemsService : IItemsService
{
    private readonly IMasterDbContext _dbContext;

    public ItemsService(IMasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask<ServiceActionResult<List<CreatedItemData>>> CreateItems(CreateItemData[] createItemsData, CancellationToken cancellationToken)
    {
        if (createItemsData.GroupBy(i => i.Name).Any(ig => ig.Count() > 1))
            return (ServiceActionResult<List<CreatedItemData>>)"One or more items have the same name!";
        
        var createdItems = new List<CreatedItemData>();

        foreach (var createItemData in createItemsData)
        {
            if (await _dbContext.Items.AsNoTracking().AnyAsync(i => i.Name == createItemData.Name, cancellationToken))
                return new ServiceActionResult<List<CreatedItemData>>($"Item with name {createItemData.Name} already exists!");

            var createdItem = _dbContext.Items.Add(new ItemDbModel
            {
                Id = Guid.NewGuid(),
                Name = createItemData.Name,
                CategoryName = createItemData.CategoryName
            }).Entity;

            createdItems.Add(new CreatedItemData(
                Id: createdItem.Id,
                Name: createdItem.Name,
                CategoryName: createdItem.CategoryName,
                CreateDate: createdItem.CreateDate));
        }

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new ServiceActionResult<List<CreatedItemData>>(createdItems);
    }
}