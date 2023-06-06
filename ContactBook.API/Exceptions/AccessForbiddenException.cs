using System;

namespace ContactBook.API.Exceptions
{
    public class AccessForbiddenException : Exception
    {
        public AccessForbiddenException() : base()
        { }

        public AccessForbiddenException(string message) : base(message)
        { }
    }
}
