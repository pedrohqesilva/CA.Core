using MediatR;
using System;

namespace Domain.Services.Events
{
    public abstract class Event : Message<bool>, INotification
    {
        public Guid Id { get; }
        public DateTime Timestamp { get; }

        protected Event()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
        }
    }
}