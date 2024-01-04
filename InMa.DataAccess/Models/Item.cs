using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess.Models;

[PrimaryKey(nameof(Id))]
[Index(nameof(Name), IsUnique = true)]
public class Item
{
    public Guid Id { get; set; }
    
    [Length(5, 100)]
    public required string Name { get; set; }
    
    [Length(1, 20)]
    public required string CategoryName { get; set; }

    public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.UtcNow;
}