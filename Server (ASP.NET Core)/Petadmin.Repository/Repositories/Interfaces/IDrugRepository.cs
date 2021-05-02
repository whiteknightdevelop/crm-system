using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for interacting with the drug backend.
    /// </summary>
    public interface IDrugRepository : IRepository<Drug>
    {
        Task<IEnumerable<string>> GetDrugsListAsync();
        Task<IEnumerable<string>> GetDrugPeriodsListAsync();
        Task<IEnumerable<string>> GetDrugFrequencysListAsync();
        Task<IEnumerable<string>> GetDrugDosagesListAsync();
    }
}
