using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Brokers.Interfaces
{
    public interface IDebtBroker
    {
        Task<Owner> GetOwnerByIdAsync(int ownerId);
        Task<IEnumerable<Debt>> GetDebtsListByOwnerIdAsync(int ownerId);
        Task<int> AddDebtAsync(Debt debt);
        Task<bool> UpdateDebtAsync(Debt debt);
        Task<bool> DeleteDebtAsync(Debt debt);
    }
}
