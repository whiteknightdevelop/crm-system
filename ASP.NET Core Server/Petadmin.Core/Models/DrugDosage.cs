using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class DrugDosage : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the dosage drug.
        /// </summary>
        public int DosageId { get; set; }

        /// <summary>
        /// Keeps the database dosage of the dosage drug.
        /// </summary>
        public string Dosage { get; set; }
    }
}
