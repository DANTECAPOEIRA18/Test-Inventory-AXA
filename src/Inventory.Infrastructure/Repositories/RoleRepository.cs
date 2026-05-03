using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Inventory.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbFactory _factory;

        public RoleRepository(DbFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<Role>> GetAll()
        {
            var list = new List<Role>();

            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_GetRoles", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new Role(
                                (Guid)reader["Id"],
                                reader["Name"].ToString()
                            ));
                        }
                    }
                }
            }

            return list;
        }

        public async Task Create(Role role)
        {
            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_CreateRole", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", role.Id);
                    cmd.Parameters.AddWithValue("@Name", role.Name);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}