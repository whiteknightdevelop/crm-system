using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetAdmin.Core.Interfaces;

namespace Petadmin.Core.Models
{
    public class PetadminStorageFile : IGenericDbEntity
    {
        public int Id { get; set; }
        public int VisitId { get; set; }
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Path { get; set; }
        public long Size { get; set; }
        public bool Exists { get; set; }
        public string FileExtension { get; set; }
        public string Hash { get; set; }
        public override string ToString()
        {
            return Id + Created.ToString(CultureInfo.CurrentCulture) + Name + Path + Size + Exists + FileExtension;
        }
    }
}
