using Microsoft.AspNetCore.Components.Web;

namespace InMa.Portal.Components.Pages.Items;

public partial class Items
{
    private int Counter = 0;
    private string _textValue => $"You hit the button {Counter} times";
    private string TextValue { get; set; } = "";

    private void ButtonClicked(MouseEventArgs args)
    {
        Counter++;
        TextValue = _textValue;
    }
}