using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<IEnumerable<string>> GetDrugsListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_drugs_list", _dbMappers.DrugMapper));
        }

        public Task<IEnumerable<string>> GetDrugPeriodsListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_drugs_period_list", _dbMappers.DrugPeriodMapper));
        }

        public Task<IEnumerable<string>> GetDrugFrequencysListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_drugs_frequency_list", _dbMappers.DrugFrequencyMapper));
        }

        public Task<IEnumerable<string>> GetDrugDosagesListAsync()
        {
            return Task.Run(() => GetListFromDbAsync("get_drugs_dosage_list", _dbMappers.DrugDosageMapper));
        }
    }
}
