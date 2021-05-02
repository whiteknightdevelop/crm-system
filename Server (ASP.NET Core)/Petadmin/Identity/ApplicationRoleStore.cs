using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Models
{
    public class ApplicationRoleStore : IRoleStore<ApplicationRole>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApplicationRoleStore> _logger;

        public ApplicationRoleStore(IUnitOfWork unitOfWork, ILogger<ApplicationRoleStore> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        private bool _disposed;
        public void Dispose() => _disposed = true;

        /// <summary>
        /// Throws if this class has been disposed.
        /// </summary>
        protected void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) { throw new ArgumentNullException(nameof(role)); }

            var ans = await _unitOfWork.Roles.AddAsync(role);
            return ans == 1 ? IdentityResult.Success : IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) { throw new ArgumentNullException(nameof(role)); }

            try
            {
                var ans = await _unitOfWork.Roles.UpdateAsync(role);
                if (!ans)
                {
                    throw new DbUpdateException();
                }
            }
            catch (DbUpdateException)
            {
                return IdentityResult.Failed();
            }
            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) { throw new ArgumentNullException(nameof(role)); }

            try
            {
                var ans = await _unitOfWork.Roles.RemoveAsync(role, cancellationToken);
                if (!ans)
                {
                    throw new DbUpdateException();
                }
            }
            catch (DbUpdateException)
            {
                return IdentityResult.Failed();
            }
            return IdentityResult.Success;
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) { throw new ArgumentNullException(nameof(role)); }

            return Task.FromResult(role.Id.ToString());
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) { throw new ArgumentNullException(nameof(role)); }

            return Task.FromResult(role.Name);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) { throw new ArgumentNullException(nameof(role)); }

            role.Name = roleName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) { throw new ArgumentNullException(nameof(role)); }

            return Task.FromResult(role.NormalizedName);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (role == null) { throw new ArgumentNullException(nameof(role)); }

            role.NormalizedName = normalizedName;
            return Task.CompletedTask;
        }

        public Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            var id = int.Parse(roleId);
            return _unitOfWork.Roles.GetAsync(id);
        }

        public Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            return _unitOfWork.Roles.FindByRoleNameAsync(normalizedRoleName, cancellationToken);
        }
    }
}
