using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Brokers.Interfaces
{
    public interface IOwnerBroker
    {
        Owner GetOwnerById(int ownerId);
        Task<Owner> GetOwnerByIdAsync(int ownerId);
        Task<IEnumerable<Animal>> GetAnimalsListByOwnerIdAsync(int ownerId);
        Task<int> GetOwnerTotalDebtAmountAsync(int ownerId);
        Task<int> AddOwnerAsync(Owner owner);
        Task<bool> UpdateOwnerAsync(Owner owner);
        Task<bool> DeleteOwnerAsync(Owner owner);
        Task<IEnumerable<Owner>> FindOwnerAsync(Owner owner);
    }
}
