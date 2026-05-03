using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Commands;
using Inventory.Application.Queries;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/areas")]
    public class AreasController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AreasController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAreasQuery());

            if (result == null)
                return NotFound("Result es NULL");

            if (!result.Any())
                return Ok("Lista vacía");

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateAreaCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }
    }
}