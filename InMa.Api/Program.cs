using InMa.Abstractions;
using InMa.Api.Endpoints;
using InMa.Contracts.Inventory;
using InMa.Contracts.Items;
using InMa.DataAccess;
using InMa.Logic;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IItemsService, ItemsService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.UseAuthorization();

app.MapGet("/", () => Results.Ok("Hello World!"));

app.MapGet(InventoryEndpointRoutes.GetInventories, Endpoints.GetInventories);
app.MapGet(InventoryEndpointRoutes.GetStorageUnits, Endpoints.GetStorageUnits);
app.MapPost(InventoryEndpointRoutes.AddStorageUnit, Endpoints.AddStorageUnit);
app.MapPost(InventoryEndpointRoutes.PostStorageCount, Endpoints.CountStorage);

app.MapGet(ItemEndpointRoutes.Get, Items.Get);
app.MapPost(ItemEndpointRoutes.Create, Items.Create);
app.MapPut(ItemEndpointRoutes.Update, () => Results.Ok());
app.MapDelete(ItemEndpointRoutes.Delete, () => Results.Ok());

app.Run();