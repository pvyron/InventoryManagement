namespace InMa.Contracts.Inventory;

public sealed class CountStorageRequestDto
{
    public Guid? StorageUnitId { get; set; }
    public required string StorageUnitName { get; set; }

    public CountStorageRequestItem[] Items { get; set; } = Array.Empty<CountStorageRequestItem>();
}

public sealed class CountStorageRequestItem
{
    public Guid? ItemId { get; set; }
    public required string ItemName { get; set; }
    public required string ItemCategory { get; set; }
    public required uint Quantity { get; set; }
}