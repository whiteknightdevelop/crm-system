using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using Petadmin.Models;

namespace Petadmin.Services.Interfaces
{
    public interface IOwnerService
    {
        Owner GetOwnerById(int ownerId);
        Task<Owner> GetOwnerByIdAsync(int ownerId);
        Task<OwnerPage> GetOwnerPageByIdAsync(int ownerId);
        Task<int> AddOwnerAsync(Owner owner);
        Task<bool> UpdateOwnerAsync(Owner owner);
        Task<bool> DeleteOwnerAsync(Owner owner);
        Task<IEnumerable<Owner>> FindOwnerAsync(Owner owner);
    }
}
