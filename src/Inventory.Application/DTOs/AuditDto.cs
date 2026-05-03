using System;

namespace Inventory.Application.DTOs
{
    public class AuditDto
    {
        public string Action { get; set; }
        public string TableName { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}