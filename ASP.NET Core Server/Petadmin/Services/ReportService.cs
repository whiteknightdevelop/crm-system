using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Exceptions;
using Petadmin.Core.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Services
{
    public class ReportService : IReportService
    {
        #region Class Initialization
        private readonly ILogger<ReportService> _logger;
        private readonly IReportBroker _reportBroker;
        public ReportService(IReportBroker reportBroker, ILogger<ReportService> logger)
        {
            _reportBroker = reportBroker;
            _logger = logger;
        }
        #endregion

        #region GET
        public Task<IEnumerable<DebtSheetItem>> GetDebtSheet()
        {
            try
            {
                return _reportBroker.GetDebtSheet();
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<IEnumerable<VisitedOwnersItem>> GetOwnersVisitedLastXDays(int days)
        {
            try
            {
                return _reportBroker.GetOwnersVisitedLastXDays(days);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<IEnumerable<VisitedOwnersItem>> GetOwnersNotVisitedLastXDays(int days)
        {
            try
            {
                return _reportBroker.GetOwnersNotVisitedLastXDays(days);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }

        public Task<IEnumerable<RabiesReport>> GetRabiesListByDateInterval(DateTime fromDate, DateTime toDate)
        {
            try
            {
                return _reportBroker.GetRabiesListByDateInterval(fromDate, toDate);
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }
        #endregion
    }
}
