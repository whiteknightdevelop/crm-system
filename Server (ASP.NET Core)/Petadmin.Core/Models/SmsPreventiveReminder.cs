using System;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class SmsPreventiveReminder : IGenericDbEntity
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Message { get; set; }

        /// <summary>
        /// Keeps the database id of the animal.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Gets or sets the treatment of the reminder.
        /// </summary>
        public string Treatment { get; set; }

        /// <summary>
        /// Keeps the reminder date.
        /// </summary>
        public DateTime? ReminderDate { get; set; }

        /// <summary>
        /// Keeps the remaining number of days.
        /// </summary>
        public int RemainingNumOfDays { get; set; }

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
        /// Gets or sets the Archive of the Animal.
        /// </summary>
        public bool AnimalArchive { get; set; }

        /// <summary>
        /// Keeps the database id of the animal owner.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the animal owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the animal owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the animal owner.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the mobile number of the animal owner.
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Gets or sets the email address of the animal owner.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the Archive of the Owner.
        /// </summary>
        public bool OwnerArchive { get; set; }
    }
}
