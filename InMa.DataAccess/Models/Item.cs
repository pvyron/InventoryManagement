using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess.Models;

[PrimaryKey("Id")]
public class Item
{
    public Guid Id { get; set; }
    
    [Length(5, 100)]
    public required string Name { get; set; }
    
    [Length(1, 20)]
    public required string CategoryName { get; set; }
    
    // [ForeignKey(nameof(ItemCategory))]
    // public required Guid ItemCategoryId { get; set; }
    // public ItemCategory Category { get; set; } = null!;
}