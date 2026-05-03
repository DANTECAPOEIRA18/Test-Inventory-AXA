using System;

namespace Inventory.CrossCutting.Helpers
{
    public static class Guard
    {
        public static void AgainstNullOrEmpty(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(message);
        }
    }
}