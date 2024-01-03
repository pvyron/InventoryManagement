using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess.Models;

[PrimaryKey(nameof(StorageUnitId), nameof(ItemId))]
public class Inventory
{
    [ForeignKey(nameof(StorageUnit))]
    public required Guid StorageUnitId { get; set; }
    public StorageUnit? StorageUnit { get; set; }
    
    [ForeignKey(nameof(Item))]
    public required Guid ItemId { get; set; }
    public Item? Item { get; set; }
    
    [DefaultValue(0)]
    public uint AvailableQuantity { get; set; }
    
    [DefaultValue(uint.MaxValue)]
    public uint MaxQuantity { get; set; }
}