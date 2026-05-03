using Inventory.CrossCutting.Configuration;
using Inventory.CrossCutting.Logging;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using Inventory.Infrastructure.Repositories;

namespace Inventory.CrossCutting.Bootstrap
{
    public static class DependencyResolver
    {
        // Infra
        public static DbFactory GetDbFactory()
        {
            return new DbFactory(AppSettings.GetConnectionString());
        }

        // Repositories
        public static IUserRepository GetUserRepository()
        {
            return new UserRepository(GetDbFactory());
        }

        public static IAreaRepository GetAreaRepository()
        {
            return new AreaRepository(GetDbFactory());
        }

        public static IRoleRepository GetRoleRepository()
        {
            return new RoleRepository(GetDbFactory());
        }

        public static IAuditRepository GetAuditRepository()
        {
            return new AuditRepository(GetDbFactory());
        }

        // Logging
        public static ILogger GetLogger()
        {
            return new FileLogger();
        }
    }
}