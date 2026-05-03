using System;

namespace Inventory.Domain.ValueObjects
{
    public class Contact
    {
        public string Value { get; private set; }

        public Contact(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Contact cannot be empty");

            Value = value;
        }
    }
}