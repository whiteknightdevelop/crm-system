using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for interacting with the prescription backend.
    /// </summary>
    public interface IPrescriptionRepository : IRepository<Prescription>
    {
        Task<IEnumerable<Prescription>> GetPrescriptionsListByVisitIdAsync(int visitId);
        Task<int> GetVisitPrescriptionsNumberAsync(int visitId);
    }
}
