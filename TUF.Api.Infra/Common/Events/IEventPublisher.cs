using Daniel.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUF.Client.Shared.Events;

namespace TUF.Api.Infra.Common.Events;

public interface IEventPublisher : ITransient
{
    Task PublishAsync(IEvent @event);
}
