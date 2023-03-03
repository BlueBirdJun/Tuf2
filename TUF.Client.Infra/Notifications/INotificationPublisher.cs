using Daniel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TUF.Client.Infra.Notifications;

public interface INotificationPublisher
{
    Task PublishAsync(INotificationMessage notification);
}
