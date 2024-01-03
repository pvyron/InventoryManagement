using InMa.Contracts.Items;
using InMa.DataAccess;
using InMa.DataAccess.Models;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InMa.Workflows.Items;

public sealed record CreateItem(CreateItemRequestModel[]? RequestModel) : IRequest<IResult>;

public sealed class CreateItemHandler : IRequestHandler<CreateItem, IResult>
{
    private readonly MasterDbContext _dbContext;

    public CreateItemHandler(MasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask<IResult> Handle(CreateItem request, CancellationToken cancellationToken)
    {
        try
        {
            if ((request.RequestModel?.Length ?? 0) <= 0)
                return Results.NoContent();

            if (request.RequestModel!.GroupBy(i => i.Name).Any(ig => ig.Count() > 1))
                return Results.BadRequest($"One or more items have the same name!");

            var createdItems = new List<CreateItemResponseModel>();

            foreach (var itemRequestModel in request.RequestModel!)
            {
                if (await _dbContext.Items.AnyAsync(i => i.Name == itemRequestModel.Name, cancellationToken))
                    return Results.BadRequest($"Item with name {itemRequestModel.Name} already exists!");

                var createdItem = _dbContext.Items.Add(new Item
                {
                    Id = Guid.NewGuid(),
                    Name = itemRequestModel.Name,
                    CategoryName = itemRequestModel.CategoryName,
                }).Entity;

                createdItems.Add(new CreateItemResponseModel(
                    Id: createdItem.Id,
                    Name: createdItem.Name,
                    CategoryName: createdItem.CategoryName,
                    CreateDate: createdItem.CreateDate));
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Results.Created("/items", createdItems);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}