using MediatR;
using System;

namespace Inventory.Application.Commands
{
    public class ActivateUserCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
    }
}
