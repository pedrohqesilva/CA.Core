using System;
using System.Runtime.Serialization;

namespace Domain.Exceptions
{
    [Serializable]
    public sealed class InvalidSpecificationException : Exception
    {
        public InvalidSpecificationException(string message)
            : base(message)
        {
        }

        private InvalidSpecificationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}