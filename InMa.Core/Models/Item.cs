using InMa.Core.Types;

namespace InMa.Core.Models;

public sealed class Item
{
    public Guid Id { get; set; }
    public required ItemName Name { get; set; }
    public required ItemCategory Category { get; set; }
}