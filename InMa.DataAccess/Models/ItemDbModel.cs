using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess.Models;

[PrimaryKey(nameof(Id))]
[Index(nameof(NameLookup), IsUnique = true)]
[Table("Items")]
public class ItemDbModel
{
    public Guid Id { get; set; }

    [MinLength(5)]
    [MaxLength(100)]
    public required string Name
    {
        get => _name!;
        set
        {
            NameLookup = value.ToLower();
            _name = value;
        } 
    }

    private string? _name;
    
    [MinLength(5)] [MaxLength(100)] public string NameLookup { get; set; } = null!;

    [MinLength(1)]
    [MaxLength(20)]
    public required string CategoryName
    {
        get => _categoryName!;
        set
        {
            CategoryNameLookup = value.ToLower();
            _categoryName = value;
        }
    }

    private string? _categoryName;

    [MinLength(1)] [MaxLength(20)] public string CategoryNameLookup { get; set; } = null!;

    public DateTimeOffset CreateDate { get; init; } = DateTimeOffset.UtcNow;
}