using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.DbContext
{
    public partial class PetadminContext
    {
        public Task<IEnumerable<DebtSheetItem>> GetDebtSheet()
        {
            return Task.Run(() => 
                GetListFromDbAsync("get_debts_sheet_list", _dbMappers.DebtSheetItemMapper).Cast<DebtSheetItem>()
            );
        }

        public Task<IEnumerable<VisitedOwnersItem>> GetOwnersVisitedLastXDays(int days)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"in_days", days}
            };
            return Task.Run(() => 
                GetListFromDbAsync("get_visited_yes_sheet_list", _dbMappers.VisitedOwnersItemMapper, mySqlCommandParameters).Cast<VisitedOwnersItem>()
            );
        }

        public Task<IEnumerable<VisitedOwnersItem>> GetOwnersNotVisitedLastXDays(int days)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"in_days", days}
            };
            return Task.Run(() => 
                GetListFromDbAsync("get_visited_no_sheet_list", _dbMappers.VisitedOwnersItemMapper, mySqlCommandParameters).Cast<VisitedOwnersItem>()
            );
        }

        public Task<IEnumerable<RabiesReport>> GetRabiesListByDateInterval(DateTime fromDate, DateTime toDate)
        {
            var mySqlCommandParameters = new Dictionary<string, object>
            {
                {"in_from_date", fromDate},
                {"in_to_date", toDate}
            };
            return Task.Run(() => 
                GetListFromDbAsync("get_preventive_rabies_by_date_interval", _dbMappers.RabiesReportMapper, mySqlCommandParameters).Cast<RabiesReport>()
            );
        }
    }
}
