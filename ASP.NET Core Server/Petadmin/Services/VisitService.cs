using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
    public class VisitService : IVisitService
    {
        #region Class Initialization
        private readonly ILogger<VisitService> _logger;
        private readonly IVisitBroker _visitBroker;
        public VisitService(IVisitBroker visitBroker, ILogger<VisitService> logger)
        {
            _visitBroker = visitBroker;
            _logger = logger;
        }
        #endregion

        #region GET
        public Task<VisitPageLists> GetVisitPageLists()
        {
            try
            {
                Task<IEnumerable<Diagnosis>> taskVisitDiagnosesList = _visitBroker.GetVisitDiagnosesListAsync();
                Task<IEnumerable<Treatment>> taskVisitTreatmentsList = _visitBroker.GetVisitTreatmentsListAsync();
                Task<IEnumerable<PreventiveTreatment>> taskPreventiveTreatmentsList = _visitBroker.GetPreventiveTreatmentsListAsync();

                return Task<VisitPageLists>.Factory.ContinueWhenAll(
                    new Task[] { taskVisitDiagnosesList, taskVisitTreatmentsList, taskPreventiveTreatmentsList },
                    tasks => new VisitPageLists
                    {
                        DiagnosisList = taskVisitDiagnosesList.Result.ToList(),
                        TreatmentsList = taskVisitTreatmentsList.Result.ToList(),
                        AllPreventiveTreatmentsList = taskPreventiveTreatmentsList.Result.ToList()
                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<VisitPage> GetVisitPageByIdAsync(int visitId)
        {
            try
            {
                Task<Visit> taskVisit = _visitBroker.GetVisitByIdAsync(visitId);
                Task<Animal> taskAnimal = _visitBroker.GetAnimalByVisitIdAsync(visitId);
                Task<Owner> taskOwner = _visitBroker.GetOwnerByVisitIdAsync(visitId);
                Task<IEnumerable<Diagnosis>> taskVisitDiagnosesList = _visitBroker.GetVisitDiagnosesListAsync();
                Task<IEnumerable<Treatment>> taskVisitTreatmentsList = _visitBroker.GetVisitTreatmentsListAsync();
                Task<IEnumerable<PreventiveTreatment>> taskPreventiveTreatmentsList = _visitBroker.GetPreventiveTreatmentsListAsync();
                Task<IEnumerable<PreventiveTreatment>> taskVisitPreventiveTreatmentsList = _visitBroker.GetPreventiveTreatmentsListByVisitIdAsync(visitId);
                Task<int> taskVisitPrescriptionsNumber = _visitBroker.GetVisitPrescriptionsNumberAsync(visitId);

                return Task<VisitPage>.Factory.ContinueWhenAll(
                    new Task[]{taskVisit, taskAnimal, taskOwner, taskVisitDiagnosesList, 
                        taskVisitTreatmentsList, taskPreventiveTreatmentsList,
                        taskVisitPreventiveTreatmentsList, taskVisitPrescriptionsNumber},
                    tasks => new VisitPage
                    {
                        Visit = taskVisit.Result,
                        Animal = taskAnimal.Result,
                        Owner = taskOwner.Result,
                        Lists = new VisitPageLists
                        {
                            DiagnosisList = taskVisitDiagnosesList.Result.ToList(),
                            TreatmentsList = taskVisitTreatmentsList.Result.ToList(),
                            AllPreventiveTreatmentsList = taskPreventiveTreatmentsList.Result.ToList(),
                        },
                        PreventiveTreatmentsList = taskVisitPreventiveTreatmentsList.Result.ToList(),
                        PrescriptionsNumber = taskVisitPrescriptionsNumber.Result,
                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<IEnumerable<PreventiveTreatment>> GetPreventiveTreatmentsListByVisitIdAsync(int visitId)
        {
            return _visitBroker.GetPreventiveTreatmentsListByVisitIdAsync(visitId);
        }
        #endregion

        #region ADD
        public Task<int> AddVisitAsync(Visit visit)
        {
            try
            {
                return _visitBroker.AddVisitAsync(visit);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new AddFailedException();
            }
        }

        public Task<int> AddPreventiveTreatmentAsync(PreventiveTreatment treatment)
        {
            try
            {
                return _visitBroker.AddPreventiveTreatmentAsync(treatment);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new AddFailedException();
            }
        }

        public async Task<int> AddPreventiveTreatmentListAsync(List<PreventiveTreatment> list)
        {
            try
            {
                List<Task<int>> taskList = new List<Task<int>>();
                foreach (var treatment in list)
                {
                    taskList.Add(_visitBroker.AddPreventiveTreatmentAsync(treatment));
                }
                int[] result = await Task.WhenAll(taskList);

                if (result.Any(ans => ans == 0))
                {
                    return 0;
                }
                return 1;
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new AddFailedException();
            }
        }
        #endregion

        #region UPDATE
        public Task<bool> UpdateVisitAsync(Visit visit)
        {
            try
            {
                return _visitBroker.UpdateVisitAsync(visit);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new UpdateFailedException();
            }
        }
        #endregion

        #region DELETE
        public Task<bool> DeleteVisitAsync(Visit visit)
        {
            try
            {
                return _visitBroker.DeleteVisitAsync(visit);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new DeleteFailedException();
            }
        }

        public Task<bool> DeleteSelectedPreventiveTreatmentsAsync(List<PreventiveTreatment> list)
        {
            try
            {
                var cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                var tasks = new List<Task<bool>>();

                foreach (var item in list)
                {
                    tasks.Add(_visitBroker.DeleteSelectedPreventiveTreatmentsAsync(item, cts, token));
                }

                return Task<bool>.Factory.ContinueWhenAll(
                    tasks.ToArray(), (results) =>
                    {
                        cts.Dispose();
                        return true;
                    }, token);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw;
            }
        }
        #endregion
    }
}
