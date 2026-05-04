using MediatR;
using Inventory.Application.Commands;
using Inventory.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers
{
    public class ActivateUserHandler : IRequestHandler<ActivateUserCommand, Unit>
    {
        private readonly IUserRepository _repo;

        public ActivateUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<Unit> Handle(ActivateUserCommand cmd, CancellationToken cancellationToken)
        {
            await _repo.Activte(cmd.UserId);

            return Unit.Value;
        }
    }
}