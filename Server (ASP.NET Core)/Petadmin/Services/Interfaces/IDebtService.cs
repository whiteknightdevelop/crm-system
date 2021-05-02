using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Models;

namespace Petadmin.Services.Interfaces
{
    public interface IDebtService
    {
        Task<DebtPage> GetDebtPageByOwnerIdAsync(int ownerId);
        Task<IEnumerable<Debt>> GetDebtsListByOwnerIdAsync(int ownerId);
        Task<int> AddDebtAsync(Debt debt);
        Task<bool> UpdateDebtAsync(Debt debt);
        Task<bool> DeleteDebtAsync(Debt debt);
    }
}
