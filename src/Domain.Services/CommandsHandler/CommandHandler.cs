using Domain.Services.Commands;
using Domain.Services.Events;
using Domain.Services.Interfaces.MediatR;
using Domain.Services.Validations;
using FluentValidation.Results;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Services.CommandsHandler
{
    public abstract class CommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : Command<TResponse>
    {
        protected readonly INotificationHandler<DomainNotification> _notificationHandler;

        protected readonly IMediatorHandler _bus;
        private readonly CommandHandlerValidation<TCommand, TResponse> _commandValidation;
        private ValidationResult _validationResult;

        public CommandHandler(IMediatorHandler bus, INotificationHandler<DomainNotification> notificationHandler, CommandHandlerValidation<TCommand, TResponse> commandValidation)
        {
            _bus = bus;
            _commandValidation = commandValidation;
            _notificationHandler = notificationHandler;
        }

        public async Task<bool> IsValidAsync(TCommand command, CancellationToken cancellationToken)
        {
            _validationResult = await _commandValidation.ValidateAsync(command, cancellationToken);

            if (!_validationResult.IsValid)
            {
                await NotifyValidationErrorsAsync(cancellationToken);
            }

            return _validationResult.IsValid;
        }

        public IList<ValidationFailure> GetValidations()
        {
            return _validationResult.Errors;
        }

        protected async Task NotifyValidationErrorsAsync(CancellationToken cancellationToken)
        {
            foreach (var error in _validationResult.Errors)
            {
                var notification = new DomainNotification(GetMessageType(), error.ErrorMessage);
                await _bus.RaiseNotificationAsync(notification, cancellationToken);
            }
        }

        private string GetMessageType()
        {
            return typeof(TCommand).Name;
        }

        public abstract Task<TResponse> Handle(TCommand command, CancellationToken cancellationToken);
    }
}