using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Queries;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/type-documents")]
    public class TypeDocumentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TypeDocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetTypeDocumentsQuery());

            if (result == null)
                return NotFound("Result es NULL");

            if (!result.Any())
                return Ok("Lista vacía");

            return Ok(result);
        }
    }
}