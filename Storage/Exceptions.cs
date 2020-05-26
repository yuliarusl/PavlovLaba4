using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication3.Storage
{
    [System.Serializable]
    public class IncorrectPhonesDataException : System.Exception
    {
        public IncorrectPhonesDataException() { }
        public IncorrectPhonesDataException(string message) : base(message) { }
        public IncorrectPhonesDataException(string message, System.Exception inner) : base(message, inner) { }
        protected IncorrectPhonesDataException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
