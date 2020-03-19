using Domain.Services.Events;

namespace Domain.Services.Commands
{
    public class Command<TResponse> : Message<TResponse>
    {
        protected Command()
        {
        }
    }
}