using InMa.Abstractions;
using InMa.DataAccess;
using InMa.DataAccess.Models;
using InMa.Logic;
using Microsoft.EntityFrameworkCore;

namespace InNa.Logic.Tests.Unit;

public class ItemsServiceTests
{
    private readonly IMasterDbContext _dbContext;
    private readonly IItemsService _itemsService;
    private readonly Faker _faker;

    public ItemsServiceTests()
    {
        _faker = new Faker();
        _dbContext = new MasterDbContext(new DbContextOptionsBuilder<MasterDbContext>().UseInMemoryDatabase(databaseName: "MasterDb").Options);
        _itemsService = new ItemsService(_dbContext);
    }
    
    [Fact]
    public async Task CreateItemsWithNoDuplicateNamesReturnsSuccess()
    {
        // Arrange
        var nameOne = _faker.Random.Utf16String(5, 100);
        var nameTwo = _faker.Random.Utf16String(5, 100);
        var category = _faker.Random.Utf16String(1, 20);
        
        var createItemsData = new[]
        {
            new CreateItemData(nameOne, category),
            new CreateItemData(nameTwo, category),
        };

        // Act
        var result = await _itemsService.CreateItems(createItemsData, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(createItemsData.Length, result.GetResult()?.Count);
    }

    [Fact]
    public async Task CreateItemsWithDuplicateNamesReturnsError()
    {
        // Arrange
        var name = _faker.Random.Utf16String(5, 100);
        var categoryOne = _faker.Random.Utf16String(1, 20);
        var categoryTwo = _faker.Random.Utf16String(1, 20);
        
        var createItemsData = new[]
        {
            new CreateItemData(name, categoryOne),
            new CreateItemData(name, categoryTwo),
        };

        // Act
        var result = await _itemsService.CreateItems(createItemsData, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal("One or more items have the same name!", result.GetError());
    }

    [Fact]
    public async Task CreateItemThatAlreadyExistsInDatabaseReturnsError()
    {
        // Arrange
        var name = _faker.Random.Utf16String(5, 100);
        var categoryOne = _faker.Random.Utf16String(1, 20);
        var categoryTwo = _faker.Random.Utf16String(1, 20);
        
        var existingItemData = new ItemDbModel { Name = name, CategoryName = categoryOne };
        
        _dbContext.Items.Add(existingItemData);
        await _dbContext.SaveChangesAsync();
        
        var newItemsData = new[]
        {
            new CreateItemData(name, categoryTwo),
        };
        
        // Act
        var result = await _itemsService.CreateItems(newItemsData, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"Item with name {name} already exists!", result.GetError());
    }

    [Fact]
    public async Task CreateItemWithSmallNameReturnsError()
    {
        // Arrange
        var name = _faker.Random.Utf16String(0, 4);
        var category = _faker.Random.Utf16String(1, 20);
        
        var newItemsData = new[]
        {
            new CreateItemData(name, category),
        };
        
        // Act
        var result = await _itemsService.CreateItems(newItemsData, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"Item name needs to be at least 5 characters long!", result.GetError());
    }

    [Fact]
    public async Task CreateItemWithNoCategoryNameReturnsError()
    {
        // Arrange
        var name = _faker.Random.Utf16String(5, 100);
        var category = string.Empty;
        
        var newItemsData = new[]
        {
            new CreateItemData(name, category),
        };
        
        // Act
        var result = await _itemsService.CreateItems(newItemsData, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal($"Item category can't be empty!", result.GetError());
    }
}
