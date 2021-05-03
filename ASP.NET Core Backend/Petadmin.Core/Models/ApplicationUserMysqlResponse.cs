using Microsoft.AspNetCore.Identity;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class ApplicationUserMysqlResponse : IGenericDbEntity
    {
        /// <summary>
        /// Initializes a new instance/>.
        /// </summary>
        public ApplicationUserMysqlResponse() { }

        /// <summary>
        /// Initializes a new instance/>.
        /// </summary>
        /// <param name="userName">The user name.</param>
        public ApplicationUserMysqlResponse(string userName)
        {
            UserName = userName;
        }

        /// <summary>
        /// Gets or sets the primary key for this user.
        /// </summary>
        [PersonalData]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user name for this user.
        /// </summary>
        [ProtectedPersonalData]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        [ProtectedPersonalData]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets a telephone number for the user.
        /// </summary>
        [ProtectedPersonalData]
        public virtual string PhoneNumber { get; set; }

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
        /// Returns the username for this user.
        /// </summary>
        public override string ToString()
            => UserName;
    }
}
