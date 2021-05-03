using System;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class VisitedOwnersItem : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the visit.
        /// </summary>
        public int VisitId { get; set; }

        /// <summary>
        /// Keeps the database id of the animal.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Keeps the database id of the owner.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the time of visit of the animal.
        /// </summary>
        public DateTime? VisitTime { get; set; }

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
        /// Gets or sets the Active of the animal.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the Sterilized of the animal.
        /// </summary>
        public bool Sterilized { get; set; }

        /// <summary>
        /// Gets or sets the ChipNumber of the animal.
        /// </summary>
        public string ChipNumber { get; set; }

        /// <summary>
        /// Gets or sets the first name of the animal owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the animal owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the city of the animal owner.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the street address of the animal owner.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the house number of the animal owner.
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the apartment number of the animal owner.
        /// </summary>
        public string ApartmentNumber { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the animal owner.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Keeps the database number of days passed.
        /// </summary>
        public int NumOfDaysPassed { get; set; }
    }
}
