using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Brokers
{
    public class PrescriptionBroker : IPrescriptionBroker
    {
        private readonly IUnitOfWork _unitOfWork;
        public PrescriptionBroker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<Visit> GetVisitByIdAsync(int visitId)
        {
            return _unitOfWork.Visits.GetAsync(visitId);
        }

        public Task<Animal> GetAnimalByVisitIdAsync(int visitId)
        {
            return _unitOfWork.Animals.GetAnimalByVisitIdAsync(visitId);
        }

        public Task<Owner> GetOwnerByVisitIdAsync(int visitId)
        {
            return _unitOfWork.Owners.GetOwnerByVisitIdAsync(visitId);
        }

        public Task<IEnumerable<Prescription>> GetPrescriptionsListByVisitIdAsync(int visitId)
        {
            return _unitOfWork.Prescriptions.GetPrescriptionsListByVisitIdAsync(visitId);
        }

        public Task<IEnumerable<string>> GetDrugsListAsync()
        {
            return _unitOfWork.Drugs.GetDrugsListAsync();
        }

        public Task<IEnumerable<string>> GetDrugPeriodsListAsync()
        {
            return _unitOfWork.Drugs.GetDrugPeriodsListAsync();
        }

        public Task<IEnumerable<string>> GetDrugFrequencysListAsync()
        {
            return _unitOfWork.Drugs.GetDrugFrequencysListAsync();
        }

        public Task<IEnumerable<string>> GetDrugDosagesListAsync()
        {
            return _unitOfWork.Drugs.GetDrugDosagesListAsync();
        }

        public Task<int> AddPrescriptionAsync(Prescription prescription)
        {
            return _unitOfWork.Prescriptions.AddAsync(prescription);
        }

        public Task<bool> DeletePrescriptiontAsync(Prescription prescription)
        {
            return _unitOfWork.Prescriptions.RemoveAsync(prescription);
        }
    }
}
