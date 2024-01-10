using System.Diagnostics;
using System.Net.Http.Json;
using Bogus;
using InMa.Contracts.Items;

Console.WriteLine("Start seeding");

const string api_url = "https://localhost:7100";

HttpClient client = new()
{
    BaseAddress = new Uri(api_url)
};

Faker faker = new();

var startingPoint = Stopwatch.GetTimestamp();

var items = faker.Make(10000, () => new CreateItemRequestModel(Random.Shared.NextInt64(0, 10000) + faker.Commerce.Product() + Random.Shared.NextInt64(0, 10000), faker.Company.CompanyName()));

items = items.DistinctBy(i => i.Name).Where(i => i.Name.Length > 5 && i.CategoryName.Length <= 20).ToList();

Console.WriteLine($"Created {items.Count} items in {Stopwatch.GetElapsedTime(startingPoint).TotalMilliseconds}ms");

var response = await client.PostAsync(ItemEndpointRoutes.Create, JsonContent.Create(items));

if (!response.IsSuccessStatusCode)
{
    var error = await response.Content.ReadFromJsonAsync<string>();
    
    Console.WriteLine(error);
    Console.WriteLine($"Failed after {Stopwatch.GetElapsedTime(startingPoint).TotalMilliseconds}ms");

    return;
}

var createdItems = await response.Content.ReadFromJsonAsync<CreateItemResponseModel[]>();

if (createdItems is not null)
    foreach (var createdItem in createdItems)
    {
        Console.WriteLine(createdItem);
    }

Console.WriteLine($"Succeeded after {Stopwatch.GetElapsedTime(startingPoint).TotalMilliseconds}ms");




