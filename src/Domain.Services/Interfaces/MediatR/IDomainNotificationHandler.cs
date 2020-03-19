using Domain.Services.Events;
using MediatR;
using System;
using System.Collections.Generic;

namespace Domain.Services.Interfaces.MediatR
{
    public interface IDomainNotificationHandler : INotificationHandler<DomainNotification>, IDisposable
    {
        List<DomainNotification> GetNotifications();

        bool HasNotifications();
    }
}