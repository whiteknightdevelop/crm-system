using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Petadmin.Brokers.Interfaces;
using Petadmin.Core.Models;
using Petadmin.Repository.Interfaces;

namespace Petadmin.Brokers
{
    public class StorageBroker : IStorageBroker
    {
        private readonly IUnitOfWork _unitOfWork;

        public StorageBroker(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddVisitFileDataToDatabase(PetadminStorageFile file)
        {
            return _unitOfWork.Storage.Add(file);
        }

        public IEnumerable<PetadminStorageFile> GetVisitFilesList(int visitId)
        {
            return _unitOfWork.Storage.GetVisitFilesList(visitId);
        }

        public Task<bool> DeleteVisitFileFromDatabase(PetadminStorageFile file)
        {
            return _unitOfWork.Storage.RemoveAsync(file);
        }

        public int CountVisitAttachments(int visitId)
        {
            return _unitOfWork.Storage.CountVisitAttachments(visitId);
        }
    }
}
