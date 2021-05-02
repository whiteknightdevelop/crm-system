using System.Collections.Generic;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Models
{
    public class OwnerPage
    {
        public Owner Owner { get; set; }
        public int OwnerTotalDebtAmount { get; set; }
        public List<Animal> AnimalsList { get; set; }
    }
}
