using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess.Models;

[PrimaryKey("Id")]
public class StorageUnit
{
    public Guid Id { get; set; }
    
    
    [Length(1, 100)] 
    public required string Name { get; set; }
}