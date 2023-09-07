namespace InMa.Contracts.Inventory;

public sealed class PostInventoryDto
{
    public Guid? StorageUnitId { get; set; }
    public required string StorageUnitName { get; set; }

    public PostInventoryDetails[] Details { get; set; } = Array.Empty<PostInventoryDetails>();
}

public sealed class PostInventoryDetails
{
    public Guid? ItemId { get; set; }
    public required string ItemName { get; set; }
    public required string ItemCategory { get; set; }
    public required uint Quantity { get; set; }
    public required string UnitOfMeasurement { get; set; }
}