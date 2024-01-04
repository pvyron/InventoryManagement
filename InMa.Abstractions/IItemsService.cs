using InMa.Core.Types;

namespace InMa.Abstractions;

public interface IItemsService
{
    ValueTask<ServiceActionResult<List<CreatedItemData>>> CreateItems(CreateItemData[] createItemsData, CancellationToken cancellationToken);
    // ValueTask<Result<List<CreatedItemData>>> GetItems(Guid? id, CancellationToken cancellationToken);
    // ValueTask<Result<CreatedItemData>> UpdateItem(Guid? id, ItemData createItemData, CancellationToken cancellationToken);
    // ValueTask<Result> DeleteItem(Guid? id, CancellationToken cancellationToken);
}

public record CreateItemData(string Name, string CategoryName);
public record CreatedItemData(Guid Id, string Name, string CategoryName, DateTimeOffset CreateDate);