using InMa.Core.Types;

namespace InMa.Abstractions;

public interface IItemsService
{
    ValueTask<Result<List<CreatedItemData>>> CreateItems(CreateItemData[] createItemsData, CancellationToken cancellationToken);
}

public record CreateItemData(string Name, string CategoryName);
public record CreatedItemData(Guid Id, string Name, string CategoryName, DateTimeOffset CreateDate);