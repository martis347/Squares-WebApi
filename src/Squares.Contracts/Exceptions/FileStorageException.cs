using System;

namespace Squares.Contracts.Exceptions
{
    public class FileStorageException : Exception
    {
        public string Reason { get; private set; }

        public FileStorageException(string message, string reason) : base(message)
        {
            Reason = reason;
        }
    }
}