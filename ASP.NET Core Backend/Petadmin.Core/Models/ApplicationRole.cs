using Microsoft.AspNetCore.Identity;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    /// <summary>
    /// Represents a custom role of the microsoft identity system IdentityRole
    /// </summary>
    public sealed class ApplicationRole : IdentityRole<int>, IGenericDbEntity
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationRole"/>.
        /// </summary>
        public ApplicationRole() { }

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationRole"/>.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        public ApplicationRole(string roleName) : this()
        {
            Name = roleName;
        }
    }
}
