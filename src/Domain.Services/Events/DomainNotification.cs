namespace Domain.Services.Events
{
    public class DomainNotification : Event
    {
        public string PropertyName { get; }
        public string ErrorMessage { get; }

        public DomainNotification(string key, string value)
        {
            PropertyName = key;
            ErrorMessage = value;
        }
    }
}