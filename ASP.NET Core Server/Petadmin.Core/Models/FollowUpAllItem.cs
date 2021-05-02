using PetAdmin.Core.Interfaces;
using PetAdmin.Core.Models;

namespace Petadmin.Core.Models
{
    public class FollowUpAllItem : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the followUp details.
        /// </summary>
        public FollowUp FollowUp { get; set; }

        /// <summary>
        /// Keeps the animal details.
        /// </summary>
        public Animal Animal { get; set; }

        /// <summary>
        /// Keeps the owner details.
        /// </summary>
        public Owner Owner { get; set; }
    }
}
