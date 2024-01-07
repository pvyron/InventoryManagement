using InMa.DataAccess;
using Mediator;
using Microsoft.AspNetCore.Http;

namespace InMa.Workflows.Items;

public sealed record DeleteItemCommand(Guid Id) : IRequest<IResult>;

public sealed class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, IResult>
{
    private readonly IMasterDbContext _dbContext;

    public DeleteItemCommandHandler(IMasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask<IResult> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
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