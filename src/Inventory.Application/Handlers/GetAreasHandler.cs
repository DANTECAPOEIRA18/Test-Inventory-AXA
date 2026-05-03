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
    public class GetAreasHandler : IRequestHandler<GetAreasQuery, List<AreaDto>>
    {
        private readonly IAreaRepository _repo;

        public GetAreasHandler(IAreaRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AreaDto>> Handle(GetAreasQuery request, CancellationToken cancellationToken)
        {
            var areas = await _repo.GetAll();

            return areas.Select(a => new AreaDto
            {
                Id = a.Id,
                Name = a.Name
            }).ToList();
        }
    }
}