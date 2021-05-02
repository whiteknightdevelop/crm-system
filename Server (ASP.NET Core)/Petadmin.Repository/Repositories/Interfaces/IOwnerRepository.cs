using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Repository.Repositories.Interfaces
{
    /// <summary>
    /// Defines methods for interacting with the owners backend.
    /// </summary>
    public interface IOwnerRepository : IRepository<Owner>, IDisposable
    {
        /// <summary>
        /// Get animals list by owner id.
        /// </summary>
        Task<IEnumerable<Animal>> GetAnimalsListByOwnerIdAsync(int ownerId);

        /// <summary>
        /// Get owner by visit id.
        /// </summary>
        Task<Owner> GetOwnerByVisitIdAsync(int visitId);

        /// <summary>
        /// Search owner by parameter
        /// </summary>
        Task<IEnumerable<Owner>> FindOwnerAsync(Owner owner);

        /// <summary>
        /// Get owner total debt amount
        /// </summary>
        Task<int> GetOwnerTotalDebtAmountAsync(int ownerId);
    }
}