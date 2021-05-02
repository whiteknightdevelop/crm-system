using Petadmin.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using PetAdmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<IEnumerable<Animal>> GetAnimalsListByOwnerIdAsync(int ownerId)
        {
            return Task.Run(() =>
            {
                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "get_owner_animals_list");
                cmd.Parameters.AddWithValue("@id_num", ownerId);

                var ownerAnimalsList = _dbCommon.GenericReaderToMapperList(cmd, _dbMappers.AnimalMapper);

                connection.Close();
                return ownerAnimalsList.Cast<Animal>();
            });
        }

        public Task<Owner> GetOwnerByVisitIdAsync(int visitId)
        {
            return Task.Run(() =>
            {
                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "get_owner_by_visit_id");
                cmd.Parameters.AddWithValue("@id_num", visitId);

                var owner = _dbCommon.GenericReaderToMapper(cmd, _dbMappers.OwnerMapper);

                connection.Close();
                return owner as Owner;
            });
        }

        public Task<int> GetOwnerTotalDebtAmountAsync(int ownerId)
        {
            return Task.Run(() =>
            {
                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "get_owner_total_debt_amount");
                cmd.Parameters.AddWithValue("in_id_num", ownerId);

                var ans = 0;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ans = reader.IsDBNull(0) ? 0 : reader.GetInt32("Total_Debt");
                    }
                }

                connection.Close();
                return ans;
            });
        }

        public Task<IEnumerable<Owner>> FindOwnerAsync(Owner owner)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"owner_id_num", owner.OwnerId},
                {"id_num", owner.IdNumber},
                {"f_name", owner.FirstName},
                {"l_name", owner.LastName},
                {"city_name", owner.City},
                {"street_name", owner.Street},
                {"phone_num", owner.Phone},
                {"email_addr", owner.Email},
            };
            return Task.Run(() => 
                GetListFromDbAsync("search_owner", _dbMappers.OwnerMapper, mySqlCommandParameters).Cast<Owner>()
            );
        }
    }
}
