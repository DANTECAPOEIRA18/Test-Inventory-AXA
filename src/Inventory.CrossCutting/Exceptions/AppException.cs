using System;

namespace Inventory.CrossCutting.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message) { }
    }
}