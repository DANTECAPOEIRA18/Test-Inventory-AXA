using MediatR;
using System;

namespace Inventory.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public int DocumentNumber { get; set; }
        public Guid AreaId { get; set; }
        public Guid RoleId { get; set; }
        public Guid TypeDocumentId { get; set; }
    }
}