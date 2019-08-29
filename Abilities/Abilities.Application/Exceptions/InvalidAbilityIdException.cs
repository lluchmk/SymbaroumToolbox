using System;
using System.Runtime.Serialization;

namespace Abilities.Application.Exceptions
{
    public class InvalidAbilityIdException : Exception
    {
        public InvalidAbilityIdException() 
            : base("Invalid ability Id")
        { }

        public InvalidAbilityIdException(string message) : base(message)
        { }

        public InvalidAbilityIdException(string message, Exception innerException) : base(message, innerException)
        { }

        protected InvalidAbilityIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
