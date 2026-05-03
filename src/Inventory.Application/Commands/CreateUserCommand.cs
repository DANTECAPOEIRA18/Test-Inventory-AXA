using MediatR;
using System;

namespace Inventory.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public Guid AreaId { get; set; }
        public Guid RoleId { get; set; }
    }
}