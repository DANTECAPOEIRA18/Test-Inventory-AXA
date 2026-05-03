using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Commands;
using Inventory.Application.Queries;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/roles")]
    public class RolesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetRolesQuery());
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRoleCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}