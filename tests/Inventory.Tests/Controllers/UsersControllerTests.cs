using FluentAssertions;
using Inventory.API.Controllers;
using Inventory.Application.Commands;
using Inventory.Application.DTOs;
using Inventory.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Xunit;

namespace Inventory.API.Tests.Controllers
{
    public class UsersControllerTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UsersController _controller;

        public UsersControllerTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _controller = new UsersController(_mediatorMock.Object);
        }

        [Fact]
        public async Task Get_ShouldReturnListOfUsers()
        {
            // Arrange
            var users = new List<UserDto>
            {
                new UserDto { Id = Guid.NewGuid(), Name = "Christian", Contact = "123" }
            };

            _mediatorMock
                .Setup(m => m.Send(It.IsAny<GetUsersQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(users);

            // Act
            var result = await _controller.Get();

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(users);
        }

        [Fact]
        public async Task Create_ShouldReturnOk()
        {
            var command = new CreateUserCommand
            {
                Name = "Test",
                Contact = "123456",
                AreaId = Guid.NewGuid(),
                RoleId = Guid.NewGuid()
            };

            _mediatorMock
                .Setup(m => m.Send(command, It.IsAny<CancellationToken>()))
                .ReturnsAsync(Guid.NewGuid()); 

            var result = await _controller.Create(command);

            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Delete_ShouldCallMediator()
        {
            var userId = Guid.NewGuid();

            var result = await _controller.Delete(userId);

            _mediatorMock.Verify(m =>
                m.Send(It.Is<DeleteUserCommand>(c => c.UserId == userId),
                It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public async Task Activate_ShouldCallMediator()
        {
            var userId = Guid.NewGuid();

            var result = await _controller.Activate(userId);

            _mediatorMock.Verify(m =>
                m.Send(It.Is<ActivateUserCommand>(c => c.UserId == userId),
                It.IsAny<CancellationToken>()), Times.Once);

            result.Should().BeOfType<OkResult>();
        }
    }
}