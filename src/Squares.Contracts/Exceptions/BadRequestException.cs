using System;

namespace Squares.Contracts.Exceptions
{
    public class BadRequestException : Exception
    {
        public string Reason { get; private set; }

        public BadRequestException(string message, string reason) : base(message)
        {
            Reason = reason;
        }
    }
}