using InMa.DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace InMa.DataAccess;

public sealed class MasterDbContext : DbContext
{
    public MasterDbContext(DbContextOptions<MasterDbContext> options) : base(options)
    {
        Items = Set<Item>();
        ItemCategories = Set<ItemCategory>();
    }

    public DbSet<Item> Items { get; set; }
    public DbSet<ItemCategory> ItemCategories { get; set; }
}