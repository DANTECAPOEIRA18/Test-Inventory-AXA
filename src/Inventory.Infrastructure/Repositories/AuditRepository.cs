using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories
{
    public class AuditRepository : IAuditRepository
    {
        private readonly DbFactory _factory;

        public AuditRepository(DbFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<AuditLog>> GetAll()
        {
            var list = new List<AuditLog>();

            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_GetAudit", conn))
                {
                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new AuditLog
                            {
                                Action = reader["Action"].ToString(),
                                TableName = reader["TableName"].ToString(),
                                OldData = reader["OldData"].ToString(),
                                NewData = reader["NewData"].ToString(),
                                CreatedAt = (DateTime)reader["CreatedAt"]
                            });
                        }
                    }
                }
            }

            return list;
        }

        public async Task<List<AuditLog>> GetByUser(Guid userId)
        {
            var list = new List<AuditLog>();

            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_GetAuditByUser", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new AuditLog
                            {
                                Action = reader["Action"].ToString(),
                                TableName = reader["TableName"].ToString(),
                                OldData = reader["OldData"].ToString(),
                                NewData = reader["NewData"].ToString(),
                                CreatedAt = (DateTime)reader["CreatedAt"]
                            });
                        }
                    }
                }
            }

            return list;
        }
    }
}