using InMa.Core.Types;

namespace InMa.Core.Models;

public sealed class StorageUnit
{
    public Guid Id { get; set; }
    public required StorageUnitName Name { get; set; }
}