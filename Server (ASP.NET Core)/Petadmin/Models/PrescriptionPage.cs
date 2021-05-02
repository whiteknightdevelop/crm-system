using System.Collections.Generic;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Models
{
    public class PrescriptionPage
    {
        public Visit Visit { get; set; }
        public Animal Animal { get; set; }
        public Owner Owner { get; set; }
        public List<Prescription> PrescriptionsList { get; set; }
        public List<string> DrugsList { get; set; }
        public List<string> PeriodsList { get; set; }
        public List<string> FrequencysList { get; set; }
        public List<string> DosagesList { get; set; }
    }
}
