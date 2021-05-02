using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Petadmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<ApplicationUser> FindByUserNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                cancellationToken.ThrowIfCancellationRequested();

                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "search_user_by_username");
                cmd.Parameters.AddWithValue("@in_username", normalizedUserName);

                var user = _dbCommon.GenericReaderToMapper(cmd, _dbMappers.ApplicationUserMapper);

                connection.Close();
                return user as ApplicationUser;
            }, cancellationToken);
        }

        public Task<IEnumerable<string>> GetGendersListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_genders_list", _dbMappers.GenderMapper));
        }
    }
}
