namespace InMa.Contracts.Items;

public sealed record CreateItemRequestModel(string Name, string CategoryName);
    
public sealed record CreateItemResponseModel(Guid Id, string Name, string CategoryName, DateTimeOffset CreateDate);