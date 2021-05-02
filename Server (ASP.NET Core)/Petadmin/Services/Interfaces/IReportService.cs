using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Services.Interfaces
{
    public interface IReportService
    {
        Task<IEnumerable<DebtSheetItem>> GetDebtSheet();
        Task<IEnumerable<VisitedOwnersItem>> GetOwnersVisitedLastXDays(int days);
        Task<IEnumerable<VisitedOwnersItem>> GetOwnersNotVisitedLastXDays(int days);
        Task<IEnumerable<RabiesReport>> GetRabiesListByDateInterval(DateTime fromDate, DateTime toDate);
    }
}
