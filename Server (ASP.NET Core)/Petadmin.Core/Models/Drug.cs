using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class Drug : IGenericDbEntity
    {
        public Drug()
        {
            DrugId = 0;
            DrugName = string.Empty;
        }

        /// <summary>
        /// Keeps the database id of the drug.
        /// </summary>
        public int DrugId { get; set; }

        /// <summary>
        /// Keeps the database name of the drug.
        /// </summary>
        public string DrugName { get; set; }
    }
}
