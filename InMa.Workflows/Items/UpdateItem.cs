using InMa.Contracts.Items;
using InMa.DataAccess;
using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace InMa.Workflows.Items;

public sealed record UpdateItemCommand(Guid Id, UpdateItemRequestModel RequestModel) : IRequest<IResult>;

public sealed class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommand, IResult>
{
    private readonly IMasterDbContext _dbContext;

    public UpdateItemCommandHandler(IMasterDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async ValueTask<IResult> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var item = await _dbContext.Items.FindAsync([request.Id], cancellationToken);

            if (item is null)
                return Results.NotFound();

            if (item.Name != request.RequestModel.Name && await _dbContext.Items.AsNoTracking()
                    .AnyAsync(i => i.Name == request.RequestModel.Name, cancellationToken))
                return Results.Conflict($"Item with name {request.RequestModel.Name} already exists!");

            item.Name = request.RequestModel.Name;
            item.CategoryName = request.RequestModel.CategoryName;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Results.Ok(new UpdateItemResponseModel(
                Id: item.Id,
                Name: item.Name,
                CategoryName: item.CategoryName,
                CreateDate: item.CreateDate));
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}