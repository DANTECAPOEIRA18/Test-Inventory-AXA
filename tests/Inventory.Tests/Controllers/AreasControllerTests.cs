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

public class AreasControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly AreasController _controller;

    public AreasControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new AreasController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ShouldReturnAreas()
    {
        var areas = new List<AreaDto>
        {
            new AreaDto { Id = System.Guid.NewGuid(), Name = "IT" }
        };

        _mediatorMock
            .Setup(m => m.Send(It.IsAny<GetAreasQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(areas);

        var result = await _controller.Get();

        var ok = result as OkObjectResult;

        ok.Should().NotBeNull();
        ok.Value.Should().BeEquivalentTo(areas);
    }
}