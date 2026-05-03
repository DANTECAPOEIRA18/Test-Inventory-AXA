using MediatR;
using System;

namespace Inventory.Application.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid UserId { get; set; }
    }   
}