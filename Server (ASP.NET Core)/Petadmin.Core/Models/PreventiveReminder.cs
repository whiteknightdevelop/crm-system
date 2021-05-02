using System;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class PreventiveReminder : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the reminder.
        /// </summary>
        public int ReminderId { get; set; }

        /// <summary>
        /// Keeps the database id of the animal.
        /// </summary>
        public int AnimalId { get; set; }

        /// <summary>
        /// Keeps the database id of the visit.
        /// </summary>
        public int VisitId { get; set; }

        /// <summary>
        /// Keeps the database id of the treatment.
        /// </summary>
        public int TreatmentId { get; set; }

        /// <summary>
        /// Keeps the preventive reminder name.
        /// </summary>
        public string PreventiveReminderName { get; set; }

        /// <summary>
        /// Keeps the reminder date.
        /// </summary>
        public DateTime? ReminderDate { get; set; }

        /// <summary>
        /// Keeps the remaining number of days.
        /// </summary>
        public int RemainingNumOfDays { get; set; }

        /// <summary>
        /// Keeps the IsReminderChecked status.
        /// </summary>
        public bool IsReminderChecked { get; set; }

        /// <summary>
        /// Keeps the IsReminderSent status.
        /// </summary>
        public bool IsReminderSent { get; set; }

        /// <summary>
        /// Keeps the IsReminderDeleted status.
        /// </summary>
        public bool IsReminderDeleted { get; set; }

        /// <summary>
        /// Keeps the preventive treatment type.
        /// </summary>
        public bool PreventiveTreatmentType { get; set; }

        /// <summary>
        /// Keeps the database id of the user.
        /// </summary>
        public int UserId { get; set; }
    }
}
