﻿using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUF.Client.Shared.Notification;

namespace TUF.Api.Infra.SignalHub;

public class SignalRHub : Hub
{
    public async Task SendMessageAsync(ChatMessage message, string userName)
    {
        await Clients.All.SendAsync("ReceiveMessage", message, userName);
    }
    public async Task ChatNotificationAsync(string message, string receiverUserId, string senderUserId)
    {
        await Clients.All.SendAsync("ReceiveChatNotification", message, receiverUserId, senderUserId);
    }
}