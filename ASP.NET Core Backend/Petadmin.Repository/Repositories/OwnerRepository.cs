using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;
using Petadmin.Repository.DbContext;
using Petadmin.Repository.Repositories.Interfaces;

namespace Petadmin.Repository.Repositories
{
    /// <summary>
    /// Contains methods for interacting with the animal owner backend using 
    /// MySQL via Stored Procedures
    /// </summary>
    public class OwnerRepository : Repository<Owner>, IOwnerRepository
    {
        private readonly IPetadminDbContext _context;
        public OwnerRepository(IPetadminDbContext context) : base(context)
        {
            _context = context;
        }

        public Task<IEnumerable<Animal>> GetAnimalsListByOwnerIdAsync(int ownerId)
        {
            return _context.GetAnimalsListByOwnerIdAsync(ownerId);
        }

        public Task<Owner> GetOwnerByVisitIdAsync(int visitId)
        {
            return _context.GetOwnerByVisitIdAsync(visitId);
        }

        public Task<IEnumerable<Owner>> FindOwnerAsync(Owner owner)
        {
            return _context.FindOwnerAsync(owner);
        }

        public Task<int> GetOwnerTotalDebtAmountAsync(int ownerId)
        {
            return _context.GetOwnerTotalDebtAmountAsync(ownerId);
        }

        private bool _disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
