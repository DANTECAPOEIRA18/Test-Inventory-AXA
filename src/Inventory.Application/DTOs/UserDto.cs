using System;

namespace Inventory.Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int DocumentNumber { get; set; }
        public string AreaName { get; set; }
        public string RoleName { get; set; }
        public string TypeDocumentName { get; set; }
        public bool IsActive { get; set; }
    }
}