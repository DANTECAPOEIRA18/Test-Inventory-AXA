using MediatR;
using Inventory.Application.Commands;
using Inventory.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers
{
    public class UpdateUserContactHandler : IRequestHandler<UpdateUserContactCommand, Unit>
    {
        private readonly IUserRepository _repo;

        public UpdateUserContactHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(UpdateUserContactCommand cmd, CancellationToken cancellationToken)
        {
            if (cmd.UserId == Guid.Empty)
                throw new Exception("UserId is required");

            if (string.IsNullOrWhiteSpace(cmd.Contact))
                throw new Exception("Contact is required");

            await _repo.UpdateContact(cmd.UserId, cmd.Contact, cmd.AreaId, cmd.RoleId);

            return Unit.Value;
        }
    }
}