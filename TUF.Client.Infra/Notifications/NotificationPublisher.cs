using Daniel.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Client.Infra.Notifications;

public class NotificationPublisher : INotificationPublisher
{
    private readonly ILogger<NotificationPublisher> _logger;
    private readonly IPublisher _mediator;

    public NotificationPublisher(ILogger<NotificationPublisher> logger, IPublisher mediator) =>
        (_logger, _mediator) = (logger, mediator);

    public Task PublishAsync(INotificationMessage notification)
    {
        _logger.LogInformation("Publishing Notification : {notification}", notification.GetType().Name);
        return _mediator.Publish(CreateNotificationWrapper(notification));
    }

    private INotification CreateNotificationWrapper(INotificationMessage notification) =>
        (INotification)Activator.CreateInstance(
            typeof(NotificationWrapper<>).MakeGenericType(notification.GetType()), notification)!;
}
