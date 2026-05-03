using Inventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Create(User user);
        Task<List<User>> GetLast();
        Task UpdateContact(Guid userId, string contact, Guid AreaId, Guid RoleId);
        Task AssignArea(Guid userId, Guid areaId);
        Task Delete(Guid userId);
    }
}