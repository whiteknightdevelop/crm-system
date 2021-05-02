using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class Treatment : IGenericDbEntity
    {
        /// <summary>
        /// Gets or sets the treatment id.
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// Gets or sets the treatment name.
        /// </summary>
        public string Name { get; set; }
    }
}
