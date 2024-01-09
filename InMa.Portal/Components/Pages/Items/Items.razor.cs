using InMa.Portal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;

namespace InMa.Portal.Components.Pages.Items;

public partial class Items
{
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ItemsService ItemsService { get; set; } = null!;
    
    private string SearchName { get; set; } = "";
    private string SearchCategory { get; set; } = "";
    private bool CallingApi { get; set; } = false;

    private IQueryable<FetchedItem> FetchedItems { get; set; } = Array.Empty<FetchedItem>().AsQueryable();
    
    private async Task SearchButtonClicked(MouseEventArgs args)
    {
        try
        {
            CallingApi = true;

            var result = await ItemsService.SearchItems(SearchName, SearchCategory);

            if (!result.IsSuccess)
            {
                var dialog = await DialogService.ShowErrorAsync(result.GetError());

                await dialog.Result;

                return;
            }

            FetchedItems = result.GetResult()!.Select(i => new FetchedItem(i.Id, i.Name, i.CategoryName, i.CreateDate)).AsQueryable();
        }
        catch (Exception ex)
        {
            var dialog = await DialogService.ShowWarningAsync(ex.Message);
            await dialog.Result;
        }
        finally
        {
            CallingApi = false;
        }
    }

    private record FetchedItem(Guid Id, string Name, string CategoryName, DateTimeOffset DateCreated);
}