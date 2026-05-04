using System;

namespace Inventory.Domain.Entities
{
    public class TypeDocument
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public TypeDocument(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("TypeDocument name required");

            Id = id;
            Name = name;
        }
    }
}