using MediatR;
using Inventory.Application.Commands;
using Inventory.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers
{
    public class AssignAreaHandler : IRequestHandler<AssignAreaCommand, Unit>
    {
        private readonly IUserRepository _repo;

        public AssignAreaHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(AssignAreaCommand cmd, CancellationToken cancellationToken)
        {
            if (cmd.UserId == Guid.Empty)
                throw new Exception("UserId is required");

            if (cmd.AreaId == Guid.Empty)
                throw new Exception("AreaId is required");

            await _repo.AssignArea(cmd.UserId, cmd.AreaId);

            return Unit.Value;
        }
    }
}