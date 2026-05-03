using System;

namespace Inventory.Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public Guid RecordId { get; set; }
        public string OldData { get; set; }
        public string NewData { get; set; }
        public string UserAction { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}