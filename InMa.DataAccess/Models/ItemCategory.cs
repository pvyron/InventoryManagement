using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess.Models;

[PrimaryKey("Id")]
public class ItemCategory
{
    public Guid Id { get; set; }
    
    [Length(1, 20)]
    public required string ItemName { get; set; }
}