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

        var createItemsDataNames = createItemsData.Select(i => i.Name.ToLower());

        if (await _dbContext.Items.AsNoTracking()
                .AnyAsync(i => createItemsDataNames.Contains(i.NameLookup), cancellationToken))
        {
            var existingItemName = await _dbContext.Items.AsNoTracking()
                .FirstOrDefaultAsync(i => createItemsDataNames.Contains(i.NameLookup), cancellationToken);
            
            return new ServiceActionResult<List<CreatedItemData>>($"Item with name {existingItemName!.Name} already exists!");
        }
        
        foreach (var createItemData in createItemsData)
        {
            if (createItemData.Name.Length < 5)
                return new ServiceActionResult<List<CreatedItemData>>("Item name needs to be at least 5 characters long!");
            
            if (string.IsNullOrWhiteSpace(createItemData.CategoryName))
                return new ServiceActionResult<List<CreatedItemData>>("Item category can't be empty!");

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