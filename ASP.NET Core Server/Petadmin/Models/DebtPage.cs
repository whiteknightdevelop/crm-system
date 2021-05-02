using System.Collections.Generic;
using Petadmin.Core.Models;

namespace Petadmin.Models
{
    public class DebtPage
    {
        public Owner Owner { get; set; }
        public List<Debt> DebtsList { get; set; }
    }
}
