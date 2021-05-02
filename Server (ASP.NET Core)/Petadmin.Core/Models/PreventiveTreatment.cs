using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class PreventiveTreatment : IGenericDbEntity
    {
        /// <summary>
        /// Gets or sets the visit id.
        /// </summary>
        public int VisitId { get; set; }

        /// <summary>
        /// Gets or sets the preventive treatment id.
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// Gets or sets the preventive treatment name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the remaining number of days till next treatment.
        /// </summary>
        public int RemainingNumOfDays { get; set; }

        /// <summary>
        /// Gets or sets the next preventive treatment name.
        /// </summary>
        public string NextTreatmentName { get; set; }

        /// <summary>
        /// Keeps the database User of the user.
        /// </summary>
        public ApplicationUserMysqlResponse User { get; set; }
    }
}
