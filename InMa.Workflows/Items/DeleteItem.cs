using InMa.DataAccess;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace InMa.Workflows.Items;

public sealed record DeleteItem(Guid Id) : IRequest<IResult>;

public sealed class DeleteItemHandler : IRequestHandler<DeleteItem, IResult>
{
    private readonly MasterDbContext _dbContext;

    public DeleteItemHandler(MasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask<IResult> Handle(DeleteItem request, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Items.FindAsync([request.Id], cancellationToken);

            if (item is null) return Results.NotFound();

            _dbContext.Items.Remove(item);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Results.Empty;
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}