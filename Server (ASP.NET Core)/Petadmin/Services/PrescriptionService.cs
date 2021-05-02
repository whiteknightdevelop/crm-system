using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Services
{
    public class PrescriptionService : IPrescriptionService
    {
        #region Class Initialization
        private readonly ILogger<PrescriptionService> _logger;
        private readonly IPrescriptionBroker _prescriptionBroker;

        public PrescriptionService(IPrescriptionBroker prescriptionBroker, ILogger<PrescriptionService> logger)
        {
            _prescriptionBroker = prescriptionBroker;
            _logger = logger;
        }
        #endregion

        #region GET
        public Task<PrescriptionPage> GetPrescriptionPageByVisitIdAsync(int visitId)
        {
            try
            {
                Task<Visit> taskVisit = _prescriptionBroker.GetVisitByIdAsync(visitId);
                Task<Animal> taskAnimal = _prescriptionBroker.GetAnimalByVisitIdAsync(visitId);
                Task<Owner> taskOwner = _prescriptionBroker.GetOwnerByVisitIdAsync(visitId);
                Task<IEnumerable<Prescription>> taskPrescriptionsList = _prescriptionBroker.GetPrescriptionsListByVisitIdAsync(visitId);
                Task<IEnumerable<string>> taskDrugsList = _prescriptionBroker.GetDrugsListAsync();
                Task<IEnumerable<string>> taskDrugPeriodsList = _prescriptionBroker.GetDrugPeriodsListAsync();
                Task<IEnumerable<string>> taskDrugFrequencysList = _prescriptionBroker.GetDrugFrequencysListAsync();
                Task<IEnumerable<string>> taskDrugDosagesList = _prescriptionBroker.GetDrugDosagesListAsync();

                return Task<PrescriptionPage>.Factory.ContinueWhenAll(
                    new Task[]
                    {
                        taskVisit, taskAnimal, taskOwner, taskPrescriptionsList,
                        taskDrugsList, taskDrugPeriodsList, taskDrugFrequencysList,
                        taskDrugDosagesList
                    },
                    tasks => new PrescriptionPage
                    {
                        Visit = taskVisit.Result,
                        Animal = taskAnimal.Result,
                        Owner = taskOwner.Result,
                        PrescriptionsList = taskPrescriptionsList.Result.ToList(),
                        DrugsList = taskDrugsList.Result.ToList(),
                        PeriodsList = taskDrugPeriodsList.Result.ToList(),
                        FrequencysList = taskDrugFrequencysList.Result.ToList(),
                        DosagesList = taskDrugDosagesList.Result.ToList(),

                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<IEnumerable<Prescription>> GetPrescriptionsListByVisitIdAsync(int visitId)
        {
            try
            {
                return _prescriptionBroker.GetPrescriptionsListByVisitIdAsync(visitId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }
        #endregion

        #region ADD
        public Task<int> AddPrescriptionAsync(Prescription prescription)
        {
            try
            {
                return _prescriptionBroker.AddPrescriptionAsync(prescription);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new AddFailedException();
            }
        }
        #endregion

        #region DELETE
        public Task<bool> DeletePrescriptiontAsync(Prescription prescription)
        {
            try
            {
                return _prescriptionBroker.DeletePrescriptiontAsync(prescription);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new DeleteFailedException();
            }
        }
        #endregion
    }
}
