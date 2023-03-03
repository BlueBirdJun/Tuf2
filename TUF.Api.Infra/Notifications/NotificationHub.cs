using Daniel.Common.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Api.Infra.Notifications;

public class NotificationHub : Hub, ITransient
{
    public NotificationHub()
    { }
    public override async Task OnConnectedAsync()
    {
        //await Groups.AddToGroupAsync(Context.ConnectionId, $"GroupTenant-{_currentTenant.Id}");
        await Groups.AddToGroupAsync(Context.ConnectionId, $"GroupTenant-1");
        await base.OnConnectedAsync();
    }
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"GroupTenant-1");
        await base.OnDisconnectedAsync(exception);
    }
}
