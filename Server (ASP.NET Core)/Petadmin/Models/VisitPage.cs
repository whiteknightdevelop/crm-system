using System.Collections.Generic;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Models
{
    public class VisitPage
    {
        public Visit Visit { get; set; }
        public Animal Animal { get; set; }
        public Owner Owner { get; set; }
        public List<PreventiveTreatment> PreventiveTreatmentsList { get; set; }
        public VisitPageLists Lists { get; set; }
        public int PrescriptionsNumber  { get; set; }
    }
}
