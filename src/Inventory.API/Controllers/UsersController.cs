using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Commands;
using Inventory.Application.Queries;

namespace Inventory.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Crear usuario
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Obtener últimos usuarios
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetUsersQuery());
            return Ok(result);
        }

        /// <summary>
        /// Actualizar contacto
        /// </summary>
        [HttpPut("{id}/contact")]
        public async Task<IActionResult> UpdateContact(Guid id, [FromBody] UpdateUserContactCommand command)
        {
            command.UserId = id;
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Asignar área
        /// </summary>
        [HttpPut("{id}/area")]
        public async Task<IActionResult> AssignArea(Guid id, [FromBody] AssignAreaCommand command)
        {
            command.UserId = id;
            await _mediator.Send(command);
            return Ok();
        }

        /// <summary>
        /// Eliminar usuario
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteUserCommand { UserId = id });
            return Ok();
        }

        /// <summary>
        /// Activar usuario
        /// </summary>
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> Activate(Guid id)
        {
            await _mediator.Send(new ActivateUserCommand { UserId = id });
            return Ok();
        }
    }
}