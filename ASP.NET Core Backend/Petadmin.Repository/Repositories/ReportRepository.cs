using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    public class ReportRepository : IReportRepository
    {
        #region Class Initialization
        private readonly IPetadminDbContext _context;
        public ReportRepository(IPetadminDbContext context)
        {
            _context = context;
        }
        #endregion

        public Task<IEnumerable<DebtSheetItem>> GetDebtSheet()
        {
            return _context.GetDebtSheet();
        }

        public Task<IEnumerable<VisitedOwnersItem>> GetOwnersVisitedLastXDays(int days)
        {
            return _context.GetOwnersVisitedLastXDays(days);
        }

        public Task<IEnumerable<VisitedOwnersItem>> GetOwnersNotVisitedLastXDays(int days)
        {
            return _context.GetOwnersNotVisitedLastXDays(days);
        }

        public Task<IEnumerable<RabiesReport>> GetRabiesListByDateInterval(DateTime fromDate, DateTime toDate)
        {
            return _context.GetRabiesListByDateInterval(fromDate, toDate);
        }
    }
}
