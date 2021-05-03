using Petadmin.Core.Interfaces;
using PetAdmin.Core.Interfaces;
using PetAdmin.Core.Models;

namespace Petadmin.Core.Models
{
    public class RabiesReport : IGenericDbEntity, IReport
    {
        public Owner Owner { get; set; }
        public Animal Animal { get; set; }
        public Visit Visit { get; set; }
    }
}
