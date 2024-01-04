namespace InMa.Contracts.Items;

public sealed record UpdateItemRequestModel(string Name, string CategoryName);

public sealed record UpdateItemResponseModel(Guid Id, string Name, string CategoryName, DateTimeOffset CreateDate);
