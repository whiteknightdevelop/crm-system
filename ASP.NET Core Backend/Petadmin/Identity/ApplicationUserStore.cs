using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Models
{
    public class ApplicationUserStore : IUserPasswordStore<ApplicationUser>, IUserRoleStore<ApplicationUser>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ApplicationUserStore(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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

        #region IUserPasswordStore
        public Task<string> GetUserIdAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.NormalizedUserName);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUser user, string normalizedName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.Run(() =>
            {
                var ans = _unitOfWork.Users.AddAsync(user).Result;
                return ans == 1 ? IdentityResult.Success : IdentityResult.Failed();
            }, cancellationToken);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.ConcurrencyStamp = Guid.NewGuid().ToString();

            try
            {
                var ans = await _unitOfWork.Users.UpdateAsync(user);
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

        public async Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            try
            {
                var ans = await _unitOfWork.Users.RemoveAsync(user, cancellationToken);
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

        public Task<ApplicationUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return _unitOfWork.Users.FindByUserNameAsync(normalizedUserName, cancellationToken);
        }

        public Task SetPasswordHashAsync(ApplicationUser user, string passwordHash, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            return Task.FromResult(user.PasswordHash != null);
        }
        #endregion

        #region IUserRoleStore
        public async Task AddToRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("ValueCannotBeNullOrEmpty", nameof(roleName));
            }

            var roleEntity = await _unitOfWork.Roles.FindByRoleNameAsync(roleName, cancellationToken);
            if (roleEntity == null)
            {
                throw new InvalidOperationException("RoleNotFound");
            }

            var ans = await _unitOfWork.UserRoles.AddAsync(CreateUserRole(user, roleEntity), cancellationToken);
            if (ans == 0)
            {
                throw new Exception("Add Role To User Failed!");
            }
        }

        private ApplicationUserRole CreateUserRole(ApplicationUser user, IdentityRole<int> role)
        {
            return new()
            {
                UserId = user.Id,
                RoleId = role.Id,
            };
        }

        public async Task RemoveFromRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("ValueCannotBeNullOrEmpty");
            }
            var roleEntity = await _unitOfWork.Roles.FindByRoleNameAsync(roleName, cancellationToken);
            if (roleEntity != null)
            {
                var userRole = new ApplicationUserRole
                {
                    UserId = user.Id,
                    RoleId = roleEntity.Id
                        
                };
                var ans = await _unitOfWork.UserRoles.RemoveAsync(userRole, cancellationToken);
                if (!ans)
                {
                    throw new Exception("Remove Role From User Failed!");
                }

            }
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            return await _unitOfWork.UserRoles.GetRolesByUserIdAsync(user.Id, cancellationToken);
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null) throw new ArgumentNullException(nameof(user));

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("ValueCannotBeNullOrEmpty");
            }
            var role = await _unitOfWork.Roles.FindByRoleNameAsync(roleName, cancellationToken);
            if (role != null)
            {
                ApplicationUserRole userRole = await _unitOfWork.UserRoles.FindUserRoleAsync(user.Id, role.Id, cancellationToken);
                return userRole != null;
            }
            return false;
        }
        #endregion
    }
}
