using System;

namespace Inventory.WPF.Models
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int DocumentNumber { get; set; }
        public Guid AreaId { get; set; }
        public Guid RoleId { get; set; }
        public Guid TypeDocumentId { get; set; }
        public string AreaName { get; set; }
        public string RoleName { get; set; }
        public string TypeDocumentName { get; set; }
        public bool IsActive { get; set; }
    }
}