using Petadmin.Core.Models;

namespace Petadmin.Core.Interfaces
{
    public interface IReport
    {
        /// <summary>
        /// Gets or sets the owner information.
        /// </summary>
        public Owner Owner { get; set; }

        /// <summary>
        /// Gets or sets the animal information.
        /// </summary>
        public Animal Animal { get; set; }

        /// <summary>
        /// Gets or sets the visit information.
        /// </summary>
        public Visit Visit { get; set; }
    }
}
