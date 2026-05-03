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
    public class AreaRepository : IAreaRepository
    {
        private readonly DbFactory _factory;

        public AreaRepository(DbFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<Area>> GetAll()
        {
            var list = new List<Area>();

            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_GetAreas", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new Area(
                                (Guid)reader["Id"],
                                reader["Name"].ToString()
                            ));
                        }
                    }
                }
            }

            return list;
        }

        public async Task Create(Area area)
        {
            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_CreateArea", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", area.Id);
                    cmd.Parameters.AddWithValue("@Name", area.Name);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}