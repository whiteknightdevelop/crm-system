using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Models;

namespace Petadmin.Services.Interfaces
{
    public interface IPrescriptionService
    {
        Task<PrescriptionPage> GetPrescriptionPageByVisitIdAsync(int visitId);
        Task<IEnumerable<Prescription>> GetPrescriptionsListByVisitIdAsync(int visitId);
        Task<int> AddPrescriptionAsync(Prescription prescription);
        Task<bool> DeletePrescriptiontAsync(Prescription prescription);
    }
}
