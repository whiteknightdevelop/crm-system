using PetAdmin.Core.Interfaces;
using PetAdmin.Core.Models;

namespace Petadmin.Core.Models
{
    public class AnimalSearch : IGenericDbEntity
    {
        public AnimalSearch()
        {
            Animal = new Animal();
            Owner = new Owner();
        }

        public Animal Animal { get; set; }
        public Owner Owner { get; set; }
    }
}
