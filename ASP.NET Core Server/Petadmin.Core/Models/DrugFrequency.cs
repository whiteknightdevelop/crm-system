using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class DrugFrequency : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the drug frequency.
        /// </summary>
        public int FrequencyId { get; set; }

        /// <summary>
        /// Keeps the database frequency of the drug.
        /// </summary>
        public string Frequency { get; set; }
    }
}
