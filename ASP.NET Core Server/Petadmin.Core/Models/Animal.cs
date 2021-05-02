using System;
using PetAdmin.Core.Interfaces;
using Petadmin.Core.Models;

namespace PetAdmin.Core.Models
{
    public class Animal : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the animal.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Keeps the database id of the animal.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Keeps the database id of the admin user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the creation date of animal.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the animal.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the breed of the animal.
        /// </summary>
        public string Breed { get; set; }

        /// <summary>
        /// Gets or sets the color of the animal.
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Gets or sets the gender of the animal.
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets the date of birth of the animal.
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the Active of the animal.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the Sterilized of the animal.
        /// </summary>
        public bool Sterilized { get; set; }

        /// <summary>
        /// Gets or sets the date of sterilization of the animal.
        /// </summary>
        public DateTime? DateOfSterilization { get; set; }

        /// <summary>
        /// Gets or sets the ChipNumber of the animal.
        /// </summary>
        public string ChipNumber { get; set; }

        /// <summary>
        /// Gets or sets the date of Chip Mark of the animal.
        /// </summary>
        public DateTime? ChipMarkDate { get; set; }

        /// <summary>
        /// Gets or sets the comment of the animal.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the Status of the animal.
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// Gets or sets the Archive of the Animal.
        /// </summary>
        public bool Archive { get; set; }

        /// <summary>
        /// Keeps the database User of the animal owner.
        /// </summary>
        public ApplicationUserMysqlResponse User { get; set; }
    }
}
