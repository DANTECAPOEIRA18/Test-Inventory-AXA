using Inventory.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Inventory.Domain.Interfaces
{
    public interface ITypeDocumentRepository
    {
        Task<List<TypeDocument>> GetAll();
        Task Create(TypeDocument document);
    }
}