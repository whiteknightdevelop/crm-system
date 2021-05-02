using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    public interface IDebtRepository : IRepository<Debt>
    {
        Task<IEnumerable<Debt>> GetDebtsListByOwnerIdAsync(int ownerId);
    }
}
