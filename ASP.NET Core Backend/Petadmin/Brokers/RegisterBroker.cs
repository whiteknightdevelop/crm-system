using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Petadmin.Brokers.Interfaces;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Brokers
{
    public class RegisterBroker : IRegisterBroker
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterBroker> _logger;
        public RegisterBroker(IUnitOfWork unitOfWork, ILogger<RegisterBroker> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Task<IEnumerable<string>> GetGendersListAsync()
        {
            return _unitOfWork.Users.GetGendersListAsync();
        }
    }
}
