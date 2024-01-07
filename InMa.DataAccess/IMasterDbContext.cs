using InMa.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess;

public interface IMasterDbContext
{
    DbSet<ItemDbModel> Items { get; set; }
    DbSet<StorageUnit> StorageUnits { get; set; }
    DbSet<Inventory> Inventories { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}