using MediatR;
using Inventory.Application.DTOs;
using Inventory.Application.Queries;
using Inventory.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Inventory.Application.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _repo;

        public GetUsersHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repo.GetLast();

            if (!string.IsNullOrEmpty(request.NameFilter))
            {
                users = users
                    .Where(u => u.Name.ToLower().Contains(request.NameFilter.ToLower()))
                    .ToList();
            }


            if (request.IsActive.HasValue)
            {
                users = users
                    .Where(u => u.IsActive == request.IsActive.Value)
                    .ToList();
            }

            return users.Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Contact = u.Contact,
                IsActive = u.IsActive,

                AreaName = u.AreaName,
                RoleName = u.RoleName,
            }).ToList();
        }
    }
}