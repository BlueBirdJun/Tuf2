using Microsoft.AspNetCore.Components;

namespace TUF.Client.Client.Components.NotiTest;

public partial class NotificationConnection
{
    [Parameter]
    public RenderFragment ChildContent { get; set; } = default!;
    protected override Task OnInitializedAsync()
    {
        return base.OnInitializedAsync();
    }
}
