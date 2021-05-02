using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class DrugPeriod : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the drug frequency.
        /// </summary>
        public int PeriodId { get; set; }

        /// <summary>
        /// Keeps the database frequency of the drug.
        /// </summary>
        public string Period { get; set; }
    }
}
