using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for interacting with the diagnosis backend.
    /// </summary>
    public interface IDiagnosisRepository : IRepository<Diagnosis>
    {
        /// <summary>
        /// Gets diagnosis list.
        /// </summary>
        Task<IEnumerable<Diagnosis>> GetDiagnosesListAsync();
    }
}
