using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Brokers
{
    public class DebtBroker : IDebtBroker
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DebtBroker> _logger;
        public DebtBroker(IUnitOfWork unitOfWork, ILogger<DebtBroker> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Task<Owner> GetOwnerByIdAsync(int ownerId)
        {
            return _unitOfWork.Owners.GetAsync(ownerId);
        }

        public Task<IEnumerable<Debt>> GetDebtsListByOwnerIdAsync(int ownerId)
        {
            return _unitOfWork.Debts.GetDebtsListByOwnerIdAsync(ownerId);
        }

        public Task<int> AddDebtAsync(Debt debt)
        {
            return _unitOfWork.Debts.AddAsync(debt);
        }

        public Task<bool> UpdateDebtAsync(Debt debt)
        {
            return _unitOfWork.Debts.UpdateAsync(debt);
        }

        public Task<bool> DeleteDebtAsync(Debt debt)
        {
            return _unitOfWork.Debts.RemoveAsync(debt);
        }
    }
}
