using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Brokers.Interfaces
{
    public interface IVisitBroker
    {
        Task<Visit> GetVisitByIdAsync(int visitId);
        Task<Animal> GetAnimalByVisitIdAsync(int visitId);
        Task<Owner> GetOwnerByVisitIdAsync(int visitId);
        Task<IEnumerable<Diagnosis>> GetVisitDiagnosesListAsync();
        Task<IEnumerable<Treatment>> GetVisitTreatmentsListAsync();
        Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListAsync();
        Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListByVisitIdAsync(int visitId);
        Task<int> AddVisitAsync(Visit visit);
        Task<int> AddPreventiveTreatmentAsync(PreventiveTreatment treatment);
        Task<bool> UpdateVisitAsync(Visit visit);
        Task<bool> DeleteVisitAsync(Visit visit);
        Task<bool> DeleteSelectedPreventiveTreatmentsAsync(PreventiveTreatment treatment, CancellationTokenSource cts, in CancellationToken token);
        Task<int> GetVisitPrescriptionsNumberAsync(int visitId);
    }
}
