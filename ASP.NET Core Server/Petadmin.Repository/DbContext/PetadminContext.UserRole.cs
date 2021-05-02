using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<ApplicationUserRole> FindUserRoleAsync(int userId, int roleEntityId,
            CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "get_userRole");
                cmd.Parameters.AddWithValue("@in_user_id", userId);
                cmd.Parameters.AddWithValue("@in_role_entity_id", roleEntityId);

                var role = _dbCommon.GenericReaderToMapper(cmd, _dbMappers.ApplicationRoleMapper);

                connection.Close();
                return role as ApplicationUserRole;
            }, cancellationToken);
        }

        public Task<List<string>> GetRolesByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var mySqlCommandParameters = new Dictionary<string, object> {{"in_user_id", userId}};
                return Task.Run(() => 
                    GetListFromDbAsync("get_user_roles_by_user_id", _dbMappers.UserRoleMapper, mySqlCommandParameters).ToList(),
                    cancellationToken);

            }, cancellationToken);

        }

        public Task<int> AddUserRoleAsync(ApplicationUserRole entity, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "add_uerRole");
                cmd.Parameters.AddWithValue("@in_user_id", entity.UserId);
                cmd.Parameters.AddWithValue("@in_role_id", entity.RoleId);

                int ans = cmd.ExecuteNonQuery();
                connection.Close();
                return ans;

            }, cancellationToken);
        }

        public Task<bool> UpdateUserRoleAsync(ApplicationUserRole entity, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "update_uerRole");
                cmd.Parameters.AddWithValue("@in_user_id", entity.UserId);
                cmd.Parameters.AddWithValue("@in_role_id", entity.RoleId);

                var ans = Convert.ToBoolean(cmd.ExecuteNonQuery());
                connection.Close();
                return ans;

            }, cancellationToken);
        }

        public Task<bool> RemoveUserRoleAsync(ApplicationUserRole entity, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                var ans = false;

                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "remove_uerRole");
                cmd.Parameters.AddWithValue("@in_user_id", entity.UserId);
                cmd.Parameters.AddWithValue("@in_role_id", entity.RoleId);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ans = reader.IsDBNull(0) || reader.GetBoolean("response");
                    }
                }
                connection.Close();
                return ans;

            }, cancellationToken);
        }
    }
}
