using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class Breed : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the animal breed.
        /// </summary>
        public string BreedId { get; set; }

        /// <summary>
        /// Gets or sets the name of the breed.
        /// </summary>
        public string BreedName { get; set; }

        /// <summary>
        /// Gets or sets the type of the animal.
        /// </summary>
        public string Type { get; set; }
    }
}
