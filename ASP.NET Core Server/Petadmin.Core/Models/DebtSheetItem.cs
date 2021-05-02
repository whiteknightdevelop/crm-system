using System;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class DebtSheetItem : IGenericDbEntity
    {
        /// <summary>
        /// Keeps the database id of the debt.
        /// </summary>
        public int DebtId { get; set; }

        /// <summary>
        /// Keeps the database id of the owner.
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// Gets or sets the first name of the owner.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the owner.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the owner.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Gets or sets the amount of the debt.
        /// </summary>
        public int DebtAmountSum { get; set; }

        /// <summary>
        /// Gets or sets the paid amount of the debt.
        /// </summary>
        public int PaidAmountSum { get; set; }

        /// <summary>
        /// Gets or sets the paid amount of the debt.
        /// </summary>
        public int TotalAmount { get; set; }

        /// <summary>
        /// Gets or sets the date of debt.
        /// </summary>
        public DateTime? DebtDate { get; set; }
    }
}
