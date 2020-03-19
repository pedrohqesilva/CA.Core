using MediatR;

namespace Domain.Services.Events
{
    public class Message<TResponse> : IRequest<TResponse>
    {
        protected string MessageType { get; set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}