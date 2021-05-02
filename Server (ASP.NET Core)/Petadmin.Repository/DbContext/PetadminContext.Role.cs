using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Petadmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<ApplicationRole> FindByRoleNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "search_role_by_name");
                cmd.Parameters.AddWithValue("@in_name", normalizedRoleName);

                var user = _dbCommon.GenericReaderToMapper(cmd, _dbMappers.ApplicationRoleMapper);

                connection.Close();
                return user as ApplicationRole;
            }, cancellationToken);
        }
    }
}
