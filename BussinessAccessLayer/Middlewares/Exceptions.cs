using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Author_API.Middlewares
{
    public class Exceptions
    {
        public class BadRequestException : Exception
        {
            public BadRequestException(string message) : base(message)
            { }
        }

        public class NotFoundException : Exception
        {
            public NotFoundException(string message) : base(message)
            { }
        }
    }
}
