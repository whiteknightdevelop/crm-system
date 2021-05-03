using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using Petadmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<IEnumerable<Prescription>> GetPrescriptionsListByVisitIdAsync(int visitId)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"id_num", visitId}
            };
            return Task.Run(() => 
                GetListFromDbAsync("get_prescription_by_visit_id", _dbMappers.PrescriptionMapper, mySqlCommandParameters).Cast<Prescription>()
            );
        }

        public Task<int> GetVisitPrescriptionsNumberAsync(int visitId)
        {
            return Task.Run(() =>
            {
                using var connection = new MySqlConnection(_dbCommon.ConnectionString);
                connection.Open();
                var cmd = _dbCommon.ProcedureQuery(connection, "get_prescription_amount_by_visit_id");
                cmd.Parameters.AddWithValue("in_id_num", visitId);

                var ans = 0;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ans = reader.IsDBNull(0) ? 0 : reader.GetInt32("Total_amount");
                    }
                }

                connection.Close();
                return ans;
            });
        }
    }
}
