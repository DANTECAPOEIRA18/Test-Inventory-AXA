using System;

namespace Inventory.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Contact { get; private set; }
        public string Email { get; private set; }
        public int DocumentNumber { get; private set; }
        public Guid AreaId { get; private set; }
        public Guid RoleId { get; private set; }
        public Guid TypeDocumentId { get; private set; }
        public bool IsActive { get; private set; }
        public string AreaName { get; set; }
        public string RoleName { get; set; }
        public string TypeDocumentName { get; set; }
        public DateTime CreatedAt { get; private set; }

        public User(
            Guid id, 
            string name, 
            string contact, 
            string email, 
            int documentNumber,
            Guid areaId, 
            Guid roleId,
            Guid typeDocumentId,
            string areaName, 
            string roleName,
            string typeDocumentName,
            bool isActive)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required");

            Id = id;
            Name = name;
            Contact = contact;
            Email = email;
            DocumentNumber = documentNumber;
            AreaId = areaId;
            RoleId = roleId;
            TypeDocumentId = typeDocumentId;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
            AreaName = areaName;
            RoleName = roleName;
            TypeDocumentName = typeDocumentName;
            IsActive = isActive;

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