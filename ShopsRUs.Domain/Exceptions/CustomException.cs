using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Exceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        public CustomException(string message)
            : this(HttpStatusCode.BadRequest, message)
        {
        }

        public CustomException(HttpStatusCode httpStatusCode, string message)
            : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }

        protected CustomException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public HttpStatusCode HttpStatusCode { get; }
    }
}
