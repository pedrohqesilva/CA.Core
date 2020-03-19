using Domain.Services.Commands;
using Domain.Services.Events;
using Domain.Services.Interfaces.MediatR;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Bus
{
    public sealed class InMemoryBus : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public InMemoryBus(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<TResponse> SendCommandAsync<TCommand, TResponse>(TCommand command, CancellationToken cancellationToken) where TCommand : Command<TResponse>
        {
            var result = await _mediator.Send(command, cancellationToken);
            return result;
        }

        public async Task RaiseEventAsync<T>(T @event, CancellationToken cancellationToken) where T : Event
        {
            try
            {
                await _mediator.Publish(@event);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task RaiseNotificationAsync(DomainNotification notification, CancellationToken cancellationToken)
        {
            return _mediator.Publish(notification, cancellationToken);
        }
    }
}