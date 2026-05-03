using MediatR;
using System;

namespace Inventory.Application.Commands
{
    public class AssignAreaCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public Guid AreaId { get; set; }
    }
}