using System;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class Owner : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the animal owner.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the id number of the animal owner.
        /// </summary>
        public string IdNumber { get; set; }

        /// <summary>
        /// Keeps the database id of the admin user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the animal owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the animal owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the animal owner.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the city of the animal owner.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the second city(2) of the animal owner.
        /// </summary>
        public string City2 { get; set; }

        /// <summary>
        /// Gets or sets the street address of the animal owner.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the second street address (2) of the animal owner.
        /// </summary>
        public string Street2 { get; set; }

        /// <summary>
        /// Gets or sets the house number of the animal owner.
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the second house number (2) of the animal owner.
        /// </summary>
        public string HouseNumber2 { get; set; }

        /// <summary>
        /// Gets or sets the apartment number of the animal owner.
        /// </summary>
        public string ApartmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the second apartment number (2) of the animal owner.
        /// </summary>
        public string ApartmentNumber2 { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the animal owner.
        /// </summary>
        public string FullAddress { get; set; }

        /// <summary>
        /// Gets or sets the postal code of the animal owner.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the second postal code (2) of the animal owner.
        /// </summary>
        public string PostalCode2 { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the animal owner.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the animal owner.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the mail box number of the animal owner.
        /// </summary>
        public int MailBox { get; set; }

        /// <summary>
        /// Gets or sets the email address of the animal owner.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the comment of the animal owner.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the creation date of owner.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the Archive of the Owner.
        /// </summary>
        public bool Archive { get; set; }

        /// <summary>
        /// Keeps the database User of the animal owner.
        /// </summary>
        public ApplicationUserMysqlResponse User { get; set; }
    }
}