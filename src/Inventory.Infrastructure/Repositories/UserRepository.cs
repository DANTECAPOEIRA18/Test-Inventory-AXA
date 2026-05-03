using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Inventory.Domain.Entities;
using Inventory.Domain.Interfaces;
using Inventory.Infrastructure.Data;

namespace Inventory.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DbFactory _factory;

        public UserRepository(DbFactory factory)
        {
            _factory = factory;
        }

        public async Task Create(User user)
        {
            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_CreateUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Id", user.Id);
                    cmd.Parameters.AddWithValue("@Name", user.Name);
                    cmd.Parameters.AddWithValue("@Contact", user.Contact);
                    cmd.Parameters.AddWithValue("@AreaId", user.AreaId);
                    cmd.Parameters.AddWithValue("@RoleId", user.RoleId);
                    cmd.Parameters.AddWithValue("@UserAction", "SYSTEM");

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<User>> GetLast()
        {
            var list = new List<User>();

            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_GetLastUsers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    await conn.OpenAsync();

                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new User(
                                (Guid)reader["Id"],
                                reader["Name"].ToString(),
                                reader["Contact"].ToString(),
                                (Guid)reader["AreaId"],
                                (Guid)reader["RoleId"],
                                reader["AreaName"].ToString(),
                                reader["RoleName"].ToString()
                            ));
                        }
                    }
                }
            }

            return list;
        }

        public async Task UpdateContact(Guid userId, string contact, Guid AreaId, Guid RoleId)
        {
            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_UpdateUserContact", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@Contact", contact);
                    cmd.Parameters.AddWithValue("@AreaId", AreaId);
                    cmd.Parameters.AddWithValue("@RoleId", RoleId);
                    cmd.Parameters.AddWithValue("@UserAction", "SYSTEM");

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task AssignArea(Guid userId, Guid areaId)
        {
            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_AssignArea", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@AreaId", areaId);
                    cmd.Parameters.AddWithValue("@UserAction", "SYSTEM");

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task Delete(Guid userId)
        {
            using (var conn = _factory.Create())
            {
                using (var cmd = new SqlCommand("sp_DeleteUser", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@UserAction", "SYSTEM");

                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }
    }
}