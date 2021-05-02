using System;
using Microsoft.AspNetCore.Identity;
using PetAdmin.Core.Interfaces;
using static System.String;

namespace Petadmin.Core.Models
{
    public class ApplicationUser : IdentityUser<int>, IGenericDbEntity
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the doctor license number.
        /// </summary>
        public string License { get; set; }

        /// <summary>
        /// Gets or sets the refresh token.
        /// </summary>
        public RefreshToken RefreshToken { get; set; }


        public bool HasValidRefreshToken(string refreshToken)
        {
            return RefreshToken.Token == refreshToken && RefreshToken.Active;
        }

        public bool HasRefreshToken(string refreshToken)
        {
            return !IsNullOrEmpty(RefreshToken.Token);
        }

        public void AddRefreshToken(string token, int userId, string remoteIpAddress, double daysToExpire=5)
        {
            RefreshToken = new RefreshToken(token, DateTime.Now.AddDays(daysToExpire), userId, remoteIpAddress);
        }

        public void RemoveRefreshToken(string refreshToken)
        {
            RefreshToken.Token = null;
        }
    }
}
