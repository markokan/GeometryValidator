using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ValidatorUtil.PDA
{
    public class InvalidPdaException : ApplicationException
    {
        public InvalidPdaException() : base()
        { }

        public InvalidPdaException(string message) : base(message)
        { }

        public InvalidPdaException(string message, Exception inner) : base(message, inner)
        { }

        public InvalidPdaException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext ctx) : base(info, ctx)
        { }
    }
}
