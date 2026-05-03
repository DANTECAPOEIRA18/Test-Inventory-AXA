using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Queries;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/audit")]
    public class AuditController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAuditQuery());
            return Ok(result);
        }
    }
}