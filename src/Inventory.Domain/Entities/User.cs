using System;

namespace Inventory.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Contact { get; private set; }
        public Guid AreaId { get; private set; }
        public Guid RoleId { get; private set; }
        public bool IsActive { get; private set; }
        public string AreaName { get; set; }
        public string RoleName { get; set; }
        public DateTime CreatedAt { get; private set; }

        public User(Guid id, string name, string contact, Guid areaId, Guid roleId, string areaName, string roleName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            Id = id;
            Name = name;
            Contact = contact;
            AreaId = areaId;
            RoleId = roleId;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            AreaName = areaName;
            RoleName = roleName;

        }

        public void UpdateContact(string contact)
        {
            if (string.IsNullOrWhiteSpace(contact))
                throw new ArgumentException("Contact is required");

            Contact = contact;
        }

        public void AssignArea(Guid areaId)
        {
            AreaId = areaId;
        }

        public void Deactivate()
        {
            IsActive = false;
        }
    }
}