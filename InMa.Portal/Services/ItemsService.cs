using InMa.Contracts.Items;
using InMa.Core.Types;

namespace InMa.Portal.Services;

public sealed class ItemsService
{
    private readonly HttpClient _httpClient;

    public ItemsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async ValueTask<ServiceActionResult<FetchedItemResponseModel[]>> GetItems()
    {
        HttpRequestMessage request = new(HttpMethod.Get, "/api/items");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return new ServiceActionResult<FetchedItemResponseModel[]>(
                (await response.Content.ReadFromJsonAsync<string>())!);

        try
        {
            var items = await response.Content.ReadFromJsonAsync<FetchedItemResponseModel[]>();

            if (items is null)
                return new ServiceActionResult<FetchedItemResponseModel[]>("Failed to deserialize");

            return new ServiceActionResult<FetchedItemResponseModel[]>(items);
        }
        catch (Exception ex)
        {
            return new ServiceActionResult<FetchedItemResponseModel[]>(
                $"{ex.Message} : {await response.Content.ReadAsStringAsync()}");
        }
    }

    public async ValueTask<ServiceActionResult<FetchedItemResponseModel>> GetItem(Guid id)
    {
        HttpRequestMessage request = new(HttpMethod.Get, $"/api/items?id={id}");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return new ServiceActionResult<FetchedItemResponseModel>(
                (await response.Content.ReadFromJsonAsync<string>())!);

        try
        {
            var item = await response.Content.ReadFromJsonAsync<FetchedItemResponseModel>();

            if (item is null)
                return new ServiceActionResult<FetchedItemResponseModel>("Failed to deserialize");

            return new ServiceActionResult<FetchedItemResponseModel>(item);
        }
        catch (Exception ex)
        {
            return new ServiceActionResult<FetchedItemResponseModel>(
                $"{ex.Message} : {await response.Content.ReadAsStringAsync()}");
        }
    }

    public async ValueTask<ServiceActionResult<FetchedItemResponseModel>> GetItem(string name)
    {
        HttpRequestMessage request = new(HttpMethod.Get, $"/api/items?name={name}");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return new ServiceActionResult<FetchedItemResponseModel>(
                (await response.Content.ReadFromJsonAsync<string>())!);

        try
        {
            var item = await response.Content.ReadFromJsonAsync<FetchedItemResponseModel>();

            if (item is null)
                return new ServiceActionResult<FetchedItemResponseModel>("Failed to deserialize");

            return new ServiceActionResult<FetchedItemResponseModel>(item);
        }
        catch (Exception ex)
        {
            return new ServiceActionResult<FetchedItemResponseModel>(
                $"{ex.Message} : {await response.Content.ReadAsStringAsync()}");
        }
    }

    public async ValueTask<ServiceActionResult<CreateItemResponseModel>> CreateItem(CreateItemRequestModel requestModel)
    {
        HttpRequestMessage request = new(HttpMethod.Post, $"/api/items")
        {
            Content = JsonContent.Create(new[] { requestModel })
        };

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return new ServiceActionResult<CreateItemResponseModel>(
                (await response.Content.ReadFromJsonAsync<string>())!);

        try
        {
            var item = await response.Content.ReadFromJsonAsync<CreateItemResponseModel[]>();

            if (item is null)
                return new ServiceActionResult<CreateItemResponseModel>("Failed to deserialize");

            return new ServiceActionResult<CreateItemResponseModel>(item[0]);
        }
        catch (Exception ex)
        {
            return new ServiceActionResult<CreateItemResponseModel>(
                $"{ex.Message} : {await response.Content.ReadAsStringAsync()}");
        }
    }
}