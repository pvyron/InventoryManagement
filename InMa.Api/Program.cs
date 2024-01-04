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

builder.Services.AddScoped<IItemsService, ItemsService>();


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

app.MapGet(InventoryEndpointRoutes.GetInventories, Endpoints.GetInventories);
app.MapGet(InventoryEndpointRoutes.GetStorageUnits, Endpoints.GetStorageUnits);
app.MapPost(InventoryEndpointRoutes.AddStorageUnit, Endpoints.AddStorageUnit);
app.MapPost(InventoryEndpointRoutes.PostStorageCount, Endpoints.CountStorage);

app.MapPost(ItemEndpointRoutes.Create, Items.Create);

app.Run();