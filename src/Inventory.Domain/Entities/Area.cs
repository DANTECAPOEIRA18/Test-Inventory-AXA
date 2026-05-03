using System;

namespace Inventory.Domain.Entities
{
    public class Area
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Area(Guid id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Area name required");

            Id = id;
            Name = name;
        }
    }
}