using Domain.Services.Commands;
using Domain.Services.Events;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services.Interfaces.MediatR
{
    public interface IMediatorHandler
    {
        Task<TResponse> SendCommandAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken) where TCommand : Command<TResponse>;

        Task RaiseEventAsync<T>(T @event, CancellationToken cancellationToken) where T : Event;

        Task RaiseNotificationAsync(DomainNotification notification, CancellationToken cancellationToken);
    }
}