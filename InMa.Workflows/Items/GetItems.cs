using InMa.Contracts.Items;
using InMa.DataAccess;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InMa.Workflows.Items;

public sealed record GetItems(Guid? Id, string? name) : IRequest<IResult>;

public sealed class GetItemsHandler : IRequestHandler<GetItems, IResult>
{
    private readonly MasterDbContext _dbContext;

    public GetItemsHandler(MasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask<IResult> Handle(GetItems request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Id is not null)
            {
                var item = await _dbContext.Items.FindAsync([request.Id], cancellationToken: cancellationToken);

                if (item is null)
                    return Results.NotFound();

                return Results.Ok(new FetchedItemResponseModel(item.Id, item.Name, item.CategoryName, item.CreateDate));
            }

            if (request.name is not null)
            {
                var item = await _dbContext.Items.FirstOrDefaultAsync(i => i.Name == request.name,
                    cancellationToken: cancellationToken);

                if (item is null)
                    return Results.NotFound();

                return Results.Ok(new FetchedItemResponseModel(item.Id, item.Name, item.CategoryName, item.CreateDate));
            }

            var items = await _dbContext.Items
                .Select(i => new FetchedItemResponseModel(i.Id, i.Name, i.CategoryName, i.CreateDate))
                .ToListAsync(cancellationToken);

            if (items.Count == 0)
                return Results.Empty;

            return Results.Ok(items);
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}