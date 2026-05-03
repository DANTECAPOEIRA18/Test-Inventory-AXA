using MediatR;
using Inventory.Application.Commands;
using Inventory.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Unit>
    {
        private readonly IUserRepository _repo;

        public DeleteUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(DeleteUserCommand cmd, CancellationToken cancellationToken)
        {
            await _repo.Delete(cmd.UserId);

            return Unit.Value;
        }
    }
}