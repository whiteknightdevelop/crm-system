using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class Prescription : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the prescription.
        /// </summary>
        public int PrescriptionId { get; set; }

        /// <summary>
        /// Keeps the database visit id of the prescription.
        /// </summary>
        public int VisitId { get; set; }

        /// <summary>
        /// Gets or sets the drug name of the prescription.
        /// </summary>
        public string DrugName { get; set; }

        /// <summary>
        /// Gets or sets the drug frequency of the prescription.
        /// </summary>
        public string DrugFrequency { get; set; }

        /// <summary>
        /// Gets or sets the drug dosage of the prescription.
        /// </summary>
        public string DrugDosage { get; set; }

        /// <summary>
        /// Gets or sets the drug period of the prescription.
        /// </summary>
        public string DrugPeriod { get; set; }

        /// <summary>
        /// Gets or sets the drug comment of the prescription.
        /// </summary>
        public string DrugComment { get; set; }
    }
}
