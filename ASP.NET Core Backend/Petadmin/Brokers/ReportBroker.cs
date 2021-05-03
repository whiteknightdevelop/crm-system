using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Brokers
{
    public class ReportBroker : IReportBroker
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportBroker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Task<IEnumerable<DebtSheetItem>> GetDebtSheet()
        {
            return _unitOfWork.Reports.GetDebtSheet();
        }

        public Task<IEnumerable<VisitedOwnersItem>> GetOwnersVisitedLastXDays(int days)
        {
            return _unitOfWork.Reports.GetOwnersVisitedLastXDays(days);
        }

        public Task<IEnumerable<VisitedOwnersItem>> GetOwnersNotVisitedLastXDays(int days)
        {
            return _unitOfWork.Reports.GetOwnersNotVisitedLastXDays(days);
        }

        public Task<IEnumerable<RabiesReport>> GetRabiesListByDateInterval(DateTime fromDate, DateTime toDate)
        {
            return _unitOfWork.Reports.GetRabiesListByDateInterval(fromDate, toDate);
        }
    }
}
