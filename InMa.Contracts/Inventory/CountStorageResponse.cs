namespace InMa.Contracts.Inventory;

public sealed class CountStorageResponseDto
{
    public required Guid StorageId { get; set; }
    public required string StorageName { get; set; }

    public CountStorageResponseItem[] Items { get; set; } = Array.Empty<CountStorageResponseItem>();
}

public sealed class CountStorageResponseItem
{
    public required Guid ItemId { get; set; }
    public required string ItemName { get; set; }
    public required uint Quantity { get; set; }
}