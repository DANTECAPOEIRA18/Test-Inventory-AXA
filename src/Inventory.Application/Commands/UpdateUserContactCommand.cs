using MediatR;
using System;

namespace Inventory.Application.Commands
{
    public class UpdateUserContactCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public Guid AreaId { get; set; }
        public Guid RoleId { get; set; }
    }
}