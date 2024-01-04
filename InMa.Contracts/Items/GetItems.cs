namespace InMa.Contracts.Items;

public sealed record FetchedItemResponseModel(Guid Id, string Name, string CategoryName, DateTimeOffset CreateDate);