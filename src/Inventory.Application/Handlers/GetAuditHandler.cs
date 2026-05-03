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
    public class GetAuditHandler : IRequestHandler<GetAuditQuery, List<AuditDto>>
    {
        private readonly IAuditRepository _repo;

        public GetAuditHandler(IAuditRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AuditDto>> Handle(GetAuditQuery request, CancellationToken cancellationToken)
        {
            List<Inventory.Domain.Entities.AuditLog> logs;

            if (request.UserId.HasValue)
            {
                logs = await _repo.GetByUser(request.UserId.Value);
            }
            else
            {
                logs = await _repo.GetAll();
            }

            return logs.Select(a => new AuditDto
            {
                Action = a.Action,
                TableName = a.TableName,
                OldData = a.OldData,
                NewData = a.NewData,
                CreatedAt = a.CreatedAt
            }).ToList();
        }
    }
}