using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<IEnumerable<Treatment>> GetTreatmentListAsync()
        {
            return Task.Run(() => 
                GetListFromDbAsync("get_treatment_list", _dbMappers.TreatmentMapper)
                    .Cast<Treatment>());
        }

        public Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListByVisitIdAsync(int visitId)
        {
           var mySqlCommandParameters = new Dictionary<string, object> {{"@id_num", visitId}};
            return Task.Run(() => 
                GetListFromDbAsync("get_preventive_treatments_by_visit_id", _dbMappers.PreventiveTreatmentMapper, mySqlCommandParameters)
                    .Cast<PreventiveTreatment>()
            );
        }

        public Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListAsync()
        {
            return Task.Run(() => 
                GetListFromDbAsync("get_all_preventive_treatments", _dbMappers.PreventiveTreatmentMapper)
                    .Cast<PreventiveTreatment>());
        }
    }
}
