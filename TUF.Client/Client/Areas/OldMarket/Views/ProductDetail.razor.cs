using Microsoft.AspNetCore.Components;
using MudBlazor;
using TUF.Client.Shared.Bodys.OldMarket;

namespace TUF.Client.Client.Areas.OldMarket.Views;

public partial class ProductDetail
{
    [CascadingParameter] MudDialogInstance MudDialog { get; set; }
    [Parameter]  public  BungaeModel?  modeldata { get; set; }


}
