using InMa.Api.Endpoints;
using InMa.Contracts.Inventory;
using InMa.DataAccess;
using Microsoft.EntityFrameworkCore;
using Mediator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MasterDbContext>(optionsAction: options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("InMaster"));
});




builder.Services.AddMediator(options =>
{
    options.ServiceLifetime = ServiceLifetime.Scoped;
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => Results.Ok("Hello World!"));

app.MapPost(EndpointRoutes.PostStorageCount, Endpoints.CountStorage);
app.MapGet(EndpointRoutes.GetInventories, Endpoints.GetInventories);

app.Run();