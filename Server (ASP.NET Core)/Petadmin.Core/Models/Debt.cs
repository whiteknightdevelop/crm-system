using System;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class Debt : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the debt.
        /// </summary>
        public int DebtId { get; set; }

        /// <summary>
        /// Keeps the database id of the debt owner.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the date of debt.
        /// </summary>
        public DateTime? DebtDate { get; set; }

        /// <summary>
        /// Gets or sets the name of the animal.
        /// </summary>
        public string AnimalName { get; set; }

        /// <summary>
        /// Gets or sets the cause of the debt.
        /// </summary>
        public string Cause { get; set; }

        /// <summary>
        /// Gets or sets the amount of the debt.
        /// </summary>
        public int DebtAmount { get; set; }

        /// <summary>
        /// Gets or sets the paid amount of the debt.
        /// </summary>
        public int PaidAmount { get; set; }
    }
}
