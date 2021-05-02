using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Services
{
    public class DebtService : IDebtService
    {
        #region Class Initialization
        private readonly ILogger<DebtService> _logger;
        private readonly IDebtBroker _debtBroker;
        public DebtService(IDebtBroker debtBroker, ILogger<DebtService> logger)
        {
            _debtBroker = debtBroker;
            _logger = logger;
        }
        #endregion

        #region GET
        public Task<DebtPage> GetDebtPageByOwnerIdAsync(int ownerId)
        {
            try
            {
                Task<Owner> taskOwner = _debtBroker.GetOwnerByIdAsync(ownerId);
                Task<IEnumerable<Debt>> taskDebtsList = _debtBroker.GetDebtsListByOwnerIdAsync(ownerId);

                return Task<DebtPage>.Factory.ContinueWhenAll(
                    new Task[]
                    {
                        taskOwner, taskDebtsList
                    },
                    tasks => new DebtPage
                    {
                        Owner = taskOwner.Result,
                        DebtsList = taskDebtsList.Result.ToList(),

                    });
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<IEnumerable<Debt>> GetDebtsListByOwnerIdAsync(int ownerId)
        {
            try
            {
                return _debtBroker.GetDebtsListByOwnerIdAsync(ownerId);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }
        #endregion

        #region ADD
        public Task<int> AddDebtAsync(Debt debt)
        {
            try
            {
                return _debtBroker.AddDebtAsync(debt);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new AddFailedException();
            }
        }
        #endregion

        #region UPDATE
        public Task<bool> UpdateDebtAsync(Debt debt)
        {
            try
            {
                return _debtBroker.UpdateDebtAsync(debt);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new UpdateFailedException();
            }
        }
        #endregion

        #region DELETE
        public Task<bool> DeleteDebtAsync(Debt debt)
        {
            try
            {
                return _debtBroker.DeleteDebtAsync(debt);
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
