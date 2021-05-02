using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class Diagnosis : IGenericDbEntity
    {
        /// <summary>
        /// Gets or sets the diagnosis id.
        /// </summary>
        public int DiagnosisId { get; set; }

        /// <summary>
        /// Gets or sets the diagnosis name.
        /// </summary>
        public string Name { get; set; }
    }
}
