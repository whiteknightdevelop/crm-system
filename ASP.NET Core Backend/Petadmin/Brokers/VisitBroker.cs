using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Brokers
{
    public class VisitBroker : IVisitBroker
    {
        #region Class Initialization
        private readonly IUnitOfWork _unitOfWork;
        public VisitBroker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region GET
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

        public Task<IEnumerable<Diagnosis>> GetVisitDiagnosesListAsync()
        {
            return _unitOfWork.Diagnosis.GetDiagnosesListAsync();
        }

        public Task<IEnumerable<Treatment>> GetVisitTreatmentsListAsync()
        {
            return _unitOfWork.Treatments.GetTreatmentListAsync();
        }

        public Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListAsync()
        {
            return _unitOfWork.Treatments.GetPreventiveTreatmentsListAsync();
        }

        public Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListByVisitIdAsync(int visitId)
        {
            return _unitOfWork.Treatments.GetPreventiveTreatmentsListByVisitIdAsync(visitId);
        }
        #endregion

        #region ADD
        public Task<int> AddVisitAsync(Visit visit)
        {
            return _unitOfWork.Visits.AddAsync(visit);
        }

        public Task<int> AddPreventiveTreatmentAsync(PreventiveTreatment treatment)
        {
            return _unitOfWork.PreventiveTreatments.AddAsync(treatment);
        }
        #endregion

        #region UPDATE
        public Task<bool> UpdateVisitAsync(Visit visit)
        {
            return _unitOfWork.Visits.UpdateAsync(visit);
        }
        #endregion

        #region DELETE
        public Task<bool> DeleteVisitAsync(Visit visit)
        {
            return _unitOfWork.Visits.RemoveAsync(visit);
        }

        public Task<bool> DeleteSelectedPreventiveTreatmentsAsync(PreventiveTreatment treatment, CancellationTokenSource cts,
            in CancellationToken token)
        {
            return _unitOfWork.PreventiveTreatments.RemoveAsync(treatment, cts, in token);
        }

        public Task<int> GetVisitPrescriptionsNumberAsync(int visitId)
        {
            return _unitOfWork.Prescriptions.GetVisitPrescriptionsNumberAsync(visitId);
        }
        #endregion
    }
}
