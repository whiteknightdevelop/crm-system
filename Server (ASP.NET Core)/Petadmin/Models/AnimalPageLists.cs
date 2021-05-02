using System.Collections.Generic;
using Petadmin.Core.Models;

namespace Petadmin.Models
{
    public class AnimalPageLists
    {
        public List<string> TypesList { get; set; }
        public List<string> GendersList { get; set; }
        public List<Breed> BreedsList { get; set; }
        public List<string> ColorsList { get; set; }
    }
}
