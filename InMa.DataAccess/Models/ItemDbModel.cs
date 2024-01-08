using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess.Models;

[PrimaryKey(nameof(Id))]
[Index(nameof(Name), IsUnique = true)]
[Table("Items")]
public class ItemDbModel
{
    public Guid Id { get; set; }
    
    [MinLength(5)]
    [MaxLength(100)]
    public required string Name { get; set; }
    
    [MinLength(1)]
    [MaxLength(20)]
    public required string CategoryName { get; set; }

    public DateTimeOffset CreateDate { get; set; } = DateTimeOffset.UtcNow;
}