using System;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class FollowUp : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the FollowUp.
        /// </summary>
        public int FollowUpId { get; set; }

        /// <summary>
        /// Keeps the database id of the animal.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Gets or sets the date of FollowUp.
        /// </summary>
        public DateTime? Date { get; set; }
        
        /// <summary>
        /// Gets or sets the Cause of the FollowUp.
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// Gets or sets the Status of the FollowUp.
        /// </summary>
        public bool Status { get; set; }
    }
}
