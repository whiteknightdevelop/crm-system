using System.Collections.Generic;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Models
{
    public class AnimalPage
    {
        public Animal Animal { get; set; }
        public Owner AnimalOwner { get; set; }
        public List<Visit> VisitsList { get; set; }
        public AnimalPageLists Lists { get; set; }
        public List<PreventiveReminder> preventiveRemindersList { get; set; }
        public List<string> RemindersList { get; set; }
    }
}
