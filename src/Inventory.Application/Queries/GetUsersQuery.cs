using MediatR;
using Inventory.Application.DTOs;
using System.Collections.Generic;

namespace Inventory.Application.Queries
{
    public class GetUsersQuery : IRequest<List<UserDto>>
    {
        public string NameFilter { get; set; }
        public bool? IsActive { get; set; }
    }
}