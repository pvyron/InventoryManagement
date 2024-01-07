using System.Threading.Tasks;
using InMa.Abstractions;
using InMa.DataAccess;
using InMa.DataAccess.Models;
using InMa.Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace InNa.Logic.Tests.Unit;

public class ItemsServiceTests
{
    [Fact]
    public async Task CreateItemsWithNoDuplicateNamesReturnsSuccess()
    {
        // Arrange
        var mockDbContext = Substitute.For<MasterDbContext>();
        var mockEntityItemDbModel = Substitute.For<EntityEntry<ItemDbModel>>();

        mockEntityItemDbModel.Entity.Returns(new ItemDbModel { Name = "Test", CategoryName = "Test" });
        mockDbContext.Items.Add(Arg.Any<ItemDbModel>()).Returns(mockEntityItemDbModel);
        mockDbContext.SaveChangesAsync().Returns(Task.FromResult(2));
        
        var itemsService = new ItemsService(mockDbContext);

        var createItemsData = new[]
        {
            new CreateItemData("Item 1", "Category 1"),
            new CreateItemData("Item 2", "Category 2"),
        };

        // Act
        var result = await itemsService.CreateItems(createItemsData, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        
        var createdItems = result.GetResult() as List<CreatedItemData>;
        Assert.Equal(createItemsData.Length, createdItems?.Count);
        
        // // Verify mock db context
        // mockDbContext.Items.AddAsync(Arg.Any<ItemDbModel>());
        // mockDbContext.SaveChangesAsync().VerifyCalledOnce();
        
    }

    [Fact]
    public async Task CreateItemsWithDuplicateNamesReturnsError()
    {
        // Arrange
        var mockDbContext = Substitute.For<MasterDbContext>();
        var itemsService = new ItemsService(mockDbContext);

        var createItemsData = new[]
        {
            new CreateItemData("Item", "Category 1"),
            new CreateItemData("Item", "Category 2"), // Duplicate name
        };

        // Act
        var result = await itemsService.CreateItems(createItemsData, CancellationToken.None);

        // Assert
        Assert.Equal("One or more items have the same name!", result.GetError());

        // Verify mock db context
        // mockDbContext.Items.ShouldHaveCalledOnce.AddAsync(It.IsAny<ItemDbModel>());
        // mockDbContext.SaveChangesAsync.ShouldNotHaveCalled();
    }

    [Fact]
    public async Task CreateItemThatAlreadyExistsInDatabaseReturnsError()
    {
        // Arrange
        var mockDbContext = Substitute.For<MasterDbContext>();
        var itemsService = new ItemsService(mockDbContext);

        var createItemsData = new[]
        {
            new CreateItemData("Existing Item", "Category 1"),
        };

        // Configure mock db context to return true for item existence
        mockDbContext.Items.AsNoTracking().AnyAsync().Returns(Task.FromResult(true));

        // Act
        var result = await itemsService.CreateItems(createItemsData, CancellationToken.None);

        // Assert
        Assert.Equal("Item with name Existing Item already exists!", result.GetError());

        // Verify mock db context
        // mockDbContext.Items.ShouldHaveCalledOnce.AddAsync(It.IsAny<ItemDbModel>());
        // mockDbContext.SaveChangesAsync.ShouldNotHaveCalled();
    }

    [Fact]
    public async Task CreateItemsSuccessfullySavesToDatabase()
    {
        // Arrange
        var mockDbContext = Substitute.For<MasterDbContext>();
        var itemsService = new ItemsService(mockDbContext);

        var createItemsData = new[]
        {
            new CreateItemData("New Item", "Category 1"),
        };

        // Configure mock db context to save successfully
        mockDbContext.SaveChangesAsync().Returns(Task.FromResult(1));

        // Act
        await itemsService.CreateItems(createItemsData, CancellationToken.None);

        // Assert
        // Verify mock db context
        // mockDbContext.Items.ShouldHaveCalledOnce.AddAsync(It.IsAny<ItemDbModel>());
        // mockDbContext.SaveChangesAsync.ShouldHaveCalledOnce();
    }
}
