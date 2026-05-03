using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IAuditRepository
    {
        Task<List<AuditLog>> GetAll();
        Task<List<AuditLog>> GetByUser(Guid userId);
    }
}