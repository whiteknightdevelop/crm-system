using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for interacting with the treatment backend.
    /// </summary>
    public interface ITreatmentRepository : IRepository<Treatment>
    {
        /// <summary>
        /// Gets treatment list.
        /// </summary>
        Task<IEnumerable<Treatment>> GetTreatmentListAsync();

        /// <summary>
        /// Gets visit preventive treatments list.
        /// </summary>
        Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListByVisitIdAsync(int visitId);

        /// <summary>
        /// Gets all preventive treatments list.
        /// </summary>
        Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListAsync();
    }
}
