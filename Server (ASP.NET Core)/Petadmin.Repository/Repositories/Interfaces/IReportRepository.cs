using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    public interface IReportRepository
    {
        Task<IEnumerable<DebtSheetItem>> GetDebtSheet();
        Task<IEnumerable<VisitedOwnersItem>> GetOwnersVisitedLastXDays(int days);
        Task<IEnumerable<VisitedOwnersItem>> GetOwnersNotVisitedLastXDays(int days);
        Task<IEnumerable<RabiesReport>> GetRabiesListByDateInterval(DateTime fromDate, DateTime toDate);
    }
}
