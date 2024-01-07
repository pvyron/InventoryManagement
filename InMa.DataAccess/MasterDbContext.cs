using InMa.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess;

public sealed class MasterDbContext : DbContext, IMasterDbContext
{
    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
        Items = Set<ItemDbModel>();
        StorageUnits = Set<StorageUnit>();
        Inventories = Set<Inventory>();
    }

    public DbSet<ItemDbModel> Items { get; set; }
    public DbSet<StorageUnit> StorageUnits { get; set; }
    public DbSet<Inventory> Inventories { get; set; }
}