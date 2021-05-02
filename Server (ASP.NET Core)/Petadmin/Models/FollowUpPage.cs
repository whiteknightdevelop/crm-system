using System.Collections.Generic;
using Petadmin.Core.Models;
using PetAdmin.Core.Models;

namespace Petadmin.Models
{
    public class FollowUpPage
    {
        public Animal Animal { get; set; }
        public Owner Owner { get; set; }
        public List<FollowUp> FollowUpsList { get; set; }
    }
}
