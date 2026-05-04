using Inventory.Application.DTOs;
using System;
using System.Collections.Generic;

namespace Inventory.API.Tests.Helpers
{
    public static class TestData
    {
        public static List<UserDto> GetUsers()
        {
            return new List<UserDto>
            {
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Christian Robayo",
                    Contact = "300123456",
                    Email = "test@mail.com",
                    AreaName = "IT",
                    RoleName = "Admin",
                    IsActive = true
                },
                new UserDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Maria Lopez",
                    Contact = "301987654",
                    Email = "maria@mail.com",
                    AreaName = "HR",
                    RoleName = "Manager",
                    IsActive = false
                }
            };
        }

        public static List<AreaDto> GetAreas()
        {
            return new List<AreaDto>
            {
                new AreaDto
                {
                    Id = Guid.NewGuid(),
                    Name = "IT"
                },
                new AreaDto
                {
                    Id = Guid.NewGuid(),
                    Name = "HR"
                }
            };
        }

        public static List<RoleDto> GetRoles()
        {
            return new List<RoleDto>
            {
                new RoleDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin"
                },
                new RoleDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Manager"
                }
            };
        }

        public static UserDto GetSingleUser()
        {
            return new UserDto
            {
                Id = Guid.NewGuid(),
                Name = "Test User",
                Contact = "123456",
                Email = "test@test.com",
                AreaName = "IT",
                RoleName = "Admin",
                IsActive = true
            };
        }
    }
}