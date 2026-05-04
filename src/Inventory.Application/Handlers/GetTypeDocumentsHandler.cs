using MediatR;
using Inventory.Application.DTOs;
using Inventory.Application.Queries;
using Inventory.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers
{
    public class GetTypeDocumentsHandler : IRequestHandler<GetTypeDocumentsQuery, List<TypeDocumentDto>>
    {
        private readonly ITypeDocumentRepository _repo;

        public GetTypeDocumentsHandler(ITypeDocumentRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TypeDocumentDto>> Handle(GetTypeDocumentsQuery request, CancellationToken cancellationToken)
        {
            var areas = await _repo.GetAll();

            return areas.Select(a => new TypeDocumentDto
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        }
    }
}