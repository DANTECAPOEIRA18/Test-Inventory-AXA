using System;

namespace Inventory.WPF.Models
{
    public class UserVm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }
        public Guid AreaId { get; set; }
        public Guid RoleId { get; set; } = Guid.Empty;
        public string AreaName { get; set; }
        public string RoleName { get; set; }
    }
}