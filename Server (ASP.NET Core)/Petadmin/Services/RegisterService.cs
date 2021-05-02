using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Exceptions;
using Petadmin.Models;
using Petadmin.Services.Interfaces;

namespace Petadmin.Services
{
    public class RegisterService : IRegisterService
    {
        #region Class Initialization
        private readonly ILogger<RegisterService> _logger;
        private readonly IRegisterBroker _registerBroker;
        public RegisterService(IRegisterBroker registerBroker, ILogger<RegisterService> logger)
        {
            _registerBroker = registerBroker;
            _logger = logger;
        }
        #endregion

        public async Task<RegisterPage> GetRegisterPageAsync()
        {
            try
            {
                return new RegisterPage
                {
                    GendersList = (await _registerBroker.GetGendersListAsync()).ToList()
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, e.Message);
                throw new GetFailedException();
            }
        }
    }
}
