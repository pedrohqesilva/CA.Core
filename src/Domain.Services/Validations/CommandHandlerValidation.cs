using Domain.Services.Commands;
using FluentValidation;

namespace Domain.Services.Validations
{
    public abstract class CommandHandlerValidation<TCommand, TResponse> : AbstractValidator<TCommand> where TCommand : Command<TResponse>
    {
    }
}