using InMa.Contracts.Items;
using InMa.Portal.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.FluentUI.AspNetCore.Components;

namespace InMa.Portal.Components.Pages.Items;

public partial class AddItem
{
    [Inject] private IMessageService MessageService { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ItemsService ItemsService { get; set; } = null!;
    
    private string NewItemName { get; set; } = "";
    private string NewItemCategory { get; set; } = "";
    private bool CallingApi { get; set; } = false;

    private async Task AddButtonClicked(MouseEventArgs args)
    {
        try
        {
            CallingApi = true;
            
            var result =
                await ItemsService.CreateItem(new CreateItemRequestModel(Name: NewItemName,
                    CategoryName: NewItemCategory));

            var message = result.IsSuccess
                ? $"Item {result.GetResult()!.Name} was successfully created"
                : result.GetError();

            var dialog = result.IsSuccess
                ? await DialogService.ShowSuccessAsync(message)
                : await DialogService.ShowErrorAsync(message);

            await dialog.Result;
            
            NewItemName = string.Empty;
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
}