using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess.Models;

[PrimaryKey("Id")]
public class Item
{
    public Guid Id { get; set; } = new();
    
    [Length(5, 100)]
    public required string ItemName { get; set; }
    
    [Length(1, 20)]
    public required string ItemCategory { get; set; }
    
    // [ForeignKey(nameof(ItemCategory))]
    // public required Guid ItemCategoryId { get; set; }
    // public ItemCategory Category { get; set; } = null!;
}