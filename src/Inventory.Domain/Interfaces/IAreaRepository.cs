using Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface IAreaRepository
    {
        Task<List<Area>> GetAll();
        Task Create(Area area);
    }
}