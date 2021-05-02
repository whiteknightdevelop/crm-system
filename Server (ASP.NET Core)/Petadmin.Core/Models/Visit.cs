using System;
using System.Collections.Generic;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class Visit : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the visit.
        /// </summary>
        public int VisitId { get; set; }

        /// <summary>
        /// Keeps the database id of the animal.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Keeps the database id of the admin user.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the time of visit of the animal.
        /// </summary>
        public DateTime? VisitTime { get; set; }

        /// <summary>
        /// Gets or sets the date of next visit of the animal.
        /// </summary>
        public DateTime? NextVisitDate { get; set; }

        /// <summary>
        /// Gets or sets the cause of the visit.
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// Gets or sets the symptoms for the visit.
        /// </summary>
        public string Symptoms { get; set; }

        /// <summary>
        /// Gets or sets the laboratory results for the visit.
        /// </summary>
        public string LabResults { get; set; }

        /// <summary>
        /// Gets or sets the comment of the visit.
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Gets or sets the temperature for the visit.
        /// </summary>
        public decimal Temperature { get; set; }

        /// <summary>
        /// Gets or sets the weight for the visit.
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// Gets or sets the pulse for the visit.
        /// </summary>
        public int Pulse { get; set; }

        /// <summary>
        /// Gets or sets the diagnosis1 of the visit.
        /// </summary>
        public string Diagnosis1 { get; set; }

        /// <summary>
        /// Gets or sets the diagnosis2 of the visit.
        /// </summary>
        public string Diagnosis2 { get; set; }

        /// <summary>
        /// Gets or sets the diagnosis3 of the visit.
        /// </summary>
        public string Diagnosis3 { get; set; }

        /// <summary>
        /// Gets or sets the treatment1 of the visit.
        /// </summary>
        public string Treatment1 { get; set; }

        /// <summary>
        /// Gets or sets the treatment2 of the visit.
        /// </summary>
        public string Treatment2 { get; set; }

        /// <summary>
        /// Gets or sets the treatment3 of the visit.
        /// </summary>
        public string Treatment3 { get; set; }

        /// <summary>
        /// Gets or sets the treatment4 of the visit.
        /// </summary>
        public string Treatment4 { get; set; }

        /// <summary>
        /// Gets or sets the treatment5 of the visit.
        /// </summary>
        public string Treatment5 { get; set; }

        /// <summary>
        /// Gets or sets the treatment6 of the visit.
        /// </summary>
        public string Treatment6 { get; set; }

        /// <summary>
        /// Gets or sets the Archive of the Visit.
        /// </summary>
        public bool Archive { get; set; }

        /// <summary>
        /// Gets or sets the PreventiveTreatmentsList of the Visit.
        /// </summary>
        public List<PreventiveTreatment> PreventiveTreatmentsList { get; set; }

        /// <summary>
        /// Keeps the database User of the animal owner.
        /// </summary>
        public ApplicationUserMysqlResponse User { get; set; }
    }
}
