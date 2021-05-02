using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.DbContext
{ 
    public partial class PetadminContext
    {
        public Task<IEnumerable<Diagnosis>> GetDiagnosesListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_diagnosis_list", _dbMappers.DiagnosisMapper).Cast<Diagnosis>());
        }
    }
}
