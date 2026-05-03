using System;

namespace Inventory.CrossCutting.Logging
{
    public interface ILogger
    {
        void Info(string message);
        void Error(string message, Exception ex = null);
    }
}