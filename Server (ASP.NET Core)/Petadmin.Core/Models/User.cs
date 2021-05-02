using System;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
 public class User : IGenericDbEntity
    {
        public User(User user)
        {
            UserId = user.UserId;
            FirstName = user.FirstName;
            LastName = user.LastName;
            Username = user.Username;
            Email = user.Email;
            Password = user.Password;
            CreatedDate = user.CreatedDate;
            Gender = user.Gender;
            License = user.License;
        }

        /// <summary>
        /// Keeps the database id of the admin user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the email address of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the date of the user creation.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the gender of the user.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the license number of the user.
        /// </summary>
        public string License { get; set; }
    }
}
