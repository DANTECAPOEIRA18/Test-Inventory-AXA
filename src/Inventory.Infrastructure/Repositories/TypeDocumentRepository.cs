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
    public class TypeDocumentRepository : ITypeDocumentRepository
    {
        private readonly DbFactory _factory;

        public TypeDocumentRepository(DbFactory factory)
        {
            _factory = factory;
        }

        public async Task<List<TypeDocument>> GetAll()
        {
            var list = new List<TypeDocument>();

            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_GetTypeDocuments", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new TypeDocument(
                                (Guid)reader["Id"],
                                reader["Name"].ToString()
                            ));
                        }
                    }
                }
            }

            return list;
        }

        public async Task Create(TypeDocument document)
        {
            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_CreateTypeDocument", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", document.Id);
                    cmd.Parameters.AddWithValue("@Name", document.Name);

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}