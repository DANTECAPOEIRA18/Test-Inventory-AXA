using Xunit;
using Moq;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Inventory.API.Controllers;
using Inventory.Application.DTOs;
using Inventory.Application.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;

public class RolesControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly RolesController _controller;

    public RolesControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new RolesController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnRoles()
    {
        var roles = new List<RoleDto>
        {
            new RoleDto { Id = System.Guid.NewGuid(), Name = "Admin" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetRolesQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(roles);

        var result = await _controller.Get();

        var ok = result as OkObjectResult;

        ok.Should().NotBeNull();
        ok.Value.Should().BeEquivalentTo(roles);
    }
}