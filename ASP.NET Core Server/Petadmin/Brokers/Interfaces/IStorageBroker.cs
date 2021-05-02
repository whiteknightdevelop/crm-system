using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petadmin.Core.Models;

namespace Petadmin.Brokers.Interfaces
{
    public interface IStorageBroker
    {
        int AddVisitFileDataToDatabase(PetadminStorageFile file);
        IEnumerable<PetadminStorageFile> GetVisitFilesList(int visitId);
        Task<bool> DeleteVisitFileFromDatabase(PetadminStorageFile file);
        int CountVisitAttachments(int visitId);
    }
}
