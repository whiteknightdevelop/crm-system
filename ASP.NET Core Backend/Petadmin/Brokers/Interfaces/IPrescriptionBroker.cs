using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Brokers.Interfaces
{
    public interface IPrescriptionBroker
    {
        Task<Visit> GetVisitByIdAsync(int visitId);
        Task<Animal> GetAnimalByVisitIdAsync(int visitId);
        Task<Owner> GetOwnerByVisitIdAsync(int visitId);
        Task<IEnumerable<Prescription>> GetPrescriptionsListByVisitIdAsync(int visitId);
        Task<IEnumerable<string>> GetDrugsListAsync();
        Task<IEnumerable<string>> GetDrugPeriodsListAsync();
        Task<IEnumerable<string>> GetDrugFrequencysListAsync();
        Task<IEnumerable<string>> GetDrugDosagesListAsync();
        Task<int> AddPrescriptionAsync(Prescription prescription);
        Task<bool> DeletePrescriptiontAsync(Prescription prescription);
    }
}
