using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Brokers
{
    public class OwnerBroker : IOwnerBroker
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OwnerBroker> _logger;
        public OwnerBroker(IUnitOfWork unitOfWork, ILogger<OwnerBroker> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public Owner GetOwnerById(int ownerId)
        {
            return _unitOfWork.Owners.Get(ownerId);
        }

        public Task<Owner> GetOwnerByIdAsync(int ownerId)
        {
            return _unitOfWork.Owners.GetAsync(ownerId);
        }

        public Task<IEnumerable<Animal>> GetAnimalsListByOwnerIdAsync(int ownerId)
        {
            return _unitOfWork.Owners.GetAnimalsListByOwnerIdAsync(ownerId);
        }

        public Task<int> GetOwnerTotalDebtAmountAsync(int ownerId)
        {
            return _unitOfWork.Owners.GetOwnerTotalDebtAmountAsync(ownerId);
        }

        public Task<int> AddOwnerAsync(Owner owner)
        {
            return _unitOfWork.Owners.AddAsync(owner);
        }

        public Task<bool> UpdateOwnerAsync(Owner owner)
        {
            return _unitOfWork.Owners.UpdateAsync(owner);
        }

        public Task<bool> DeleteOwnerAsync(Owner owner)
        {
            return _unitOfWork.Owners.RemoveAsync(owner);
        }

        public Task<IEnumerable<Owner>> FindOwnerAsync(Owner owner)
        {
            return _unitOfWork.Owners.FindOwnerAsync(owner);
        }
    }
}
