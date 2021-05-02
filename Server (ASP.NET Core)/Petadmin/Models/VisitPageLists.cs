using System.Collections.Generic;
using Petadmin.Core.Models;

namespace Petadmin.Models
{
    public class VisitPageLists
    {
        public List<Diagnosis> DiagnosisList { get; set; }
        public List<Treatment> TreatmentsList { get; set; }
        public List<PreventiveTreatment> AllPreventiveTreatmentsList { get; set; }
    }
}
