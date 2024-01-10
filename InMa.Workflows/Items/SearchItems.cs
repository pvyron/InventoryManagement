using InMa.Contracts.Items;
using InMa.DataAccess;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InMa.Workflows.Items;

public sealed record SearchItemsQuery(string? Name, string? CategoryName) : IRequest<IResult>;

public sealed class SearchItemsQueryHandler : IRequestHandler<SearchItemsQuery, IResult>
{
    private readonly IMasterDbContext _dbContext;

    public SearchItemsQueryHandler(IMasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask<IResult> Handle(SearchItemsQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Items.AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Name))
            query = query.Where(i => EF.Functions.Like(i.NameLookup, $"{request.Name!.ToLower().Replace('*', '%')}"));
        
        if (!string.IsNullOrWhiteSpace(request.CategoryName))
            query = query.Where(i => EF.Functions.Like(i.CategoryNameLookup, $"{request.CategoryName!.ToLower().Replace('*', '%')}"));

        var items = await query.Select(i => new FetchedItemResponseModel(i.Id, i.Name, i.CategoryName, i.CreateDate)).ToListAsync(cancellationToken);
        
        return Results.Ok(items);
    }
}