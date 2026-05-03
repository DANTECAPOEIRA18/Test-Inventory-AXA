using Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAll();
        Task Create(Role role);
    }
}