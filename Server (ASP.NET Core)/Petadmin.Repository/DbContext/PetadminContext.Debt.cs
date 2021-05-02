using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<IEnumerable<Debt>> GetDebtsListByOwnerIdAsync(int ownerId)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"in_id_num", ownerId}
            };
            return Task.Run(() => 
                GetListFromDbAsync("get_debts_by_owner_id", _dbMappers.DebtMapper, mySqlCommandParameters).Cast<Debt>()
            );
        }
    }
}
