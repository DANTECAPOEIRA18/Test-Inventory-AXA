using MediatR;
using Inventory.Application.Commands;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _repo;

        public CreateUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Guid> Handle(CreateUserCommand cmd, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(cmd.Name))
                throw new Exception("Name is required");

            var userId = Guid.NewGuid();

            var user = new User(
                userId,
                cmd.Name,
                cmd.Contact,
                cmd.Email,
                cmd.DocumentNumber,
                cmd.AreaId,
                cmd.RoleId,
                cmd.TypeDocumentId,
                "",
                "",
                "",
                true
            );

            await _repo.Create(user);

            return userId;
        }
    }
}