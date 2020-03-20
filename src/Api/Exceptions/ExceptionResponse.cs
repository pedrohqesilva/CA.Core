namespace Api.Exceptions
{
    public class ExeceptionResponse
    {
        public string Message { get; }
        public string InnerMessage { get; }
        public string StackTrace { get; }

        public ExeceptionResponse(string message)
        {
            Message = message;
        }

        public ExeceptionResponse(string message, string stackTrace) : this(message)
        {
            StackTrace = stackTrace;
        }

        public ExeceptionResponse(string message, string stackTrace, string innerMessage) : this(message, stackTrace)
        {
            InnerMessage = innerMessage;
        }

        public ExeceptionResponse()
        {
        }

        public override string ToString()
        {
            return $"Message: {Message} - InnerMessage: {InnerMessage} - StackTrace: {StackTrace}";
        }
    }
}