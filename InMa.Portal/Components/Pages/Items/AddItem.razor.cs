using InMa.Contracts.Items;
using InMa.Portal.Services;
using Microsoft.AspNetCore.Components;

namespace InMa.Portal.Components.Pages.Items;

public partial class AddItem
{
    [Inject] private ItemsService ItemsService { get; set; }
    private string NewItemName { get; set; } = "";
    private string NewItemCategory { get; set; } = "";
    
    public async void OnAdd()
    {
        try
        {
            var pew = await ItemsService.CreateItem(new CreateItemRequestModel(Name: NewItemName, CategoryName: NewItemCategory));
            Console.WriteLine(pew.IsSuccess);

            var line = pew.IsSuccess ? pew.GetResult()!.ToString() : pew.GetError();
            Console.WriteLine(line);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}